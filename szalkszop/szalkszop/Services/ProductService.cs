using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IProductCategoryService _productCategoryService;
		private readonly IProductImageService _productImageService;

		public ProductService(IProductRepository productRepository, IProductCategoryService productCategoryRepository, IProductImageService productImageService)
		{
			_productRepository = productRepository;
			_productCategoryService = productCategoryRepository;
			_productImageService = productImageService;
		}

		public IEnumerable<ProductDto> GetProducts()
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
				.Include(i => i.Images).ToList();

			var productsDto = ProductMapper.MapToDto(products);

			return productsDto;
		}

		public ProductSearchViewModel GetProductSearchViewModel()
		{
			var viewModel = new ProductSearchViewModel
			{
				ProductCategories = _productCategoryService.GetProductCategoriesList(),
			};

			return viewModel;
		}

		public IEnumerable<ProductSearchResult> GetQueriedProducts(ProductSearchViewModel searchModel)
		{
			IEnumerable<ProductSearchResult> products;

			return products = _productRepository.SearchResultFromSqlStoredProcedure(searchModel.Name,
				searchModel.PriceFrom,
				searchModel.PriceTo,
				searchModel.DateTimeTo,
				searchModel.DateTimeFrom,
				searchModel.ProductCategory.Id);
		}

		public ProductViewModel AddProductViewModel()
		{
			var viewModel = new ProductViewModel
			{
				ProductCategoriesDto = _productCategoryService.GetProductCategoriesList(),
			};

			return viewModel;
		}

		public ProductViewModel EditProductViewModel(int id)
		{
			var product = _productRepository.Get(id);

			var productDto = ProductMapper.MapToDto(product);

			var viewModel = new ProductViewModel
			{
				Id = productDto.Id,
				Name = productDto.Name,
				ProductCategoriesDto = _productCategoryService.GetProductCategoriesList(),
				ProductCategory = productDto.ProductCategoryId,
				AmountInStock = productDto.AmountInStock,
				Price = productDto.Price,
				Description = productDto.Description,
				ProductImagesDto = productDto.Images,
			};

			return viewModel;
		}

		public IEnumerable<ProductDto> GetThreeNewestProducts()
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
				.Include(i => i.Images)
				.OrderByDescending(d => d.DateOfAdding)
				.Take(3)
				.ToList();

			var productsDto = ProductMapper.MapToDto(products);

			return productsDto;
		}

		public ProductsViewModel GetProductsByCategoryViewModel(int categoryId)
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
				.Include(i => i.Images)
				.Where(p => p.ProductCategoryId == categoryId)
				.ToList();

			var productsDto = ProductMapper.MapToDto(products);

			var viewModel = new ProductsViewModel
			{
				ProductsDto = productsDto,
			};

			return viewModel;
		}

		public ProductDetailViewModel ProductDetailViewModel(int id)
		{
			var product = _productRepository.Get(id);

			var productDto = ProductMapper.MapToDto(product);

			var viewModel = new ProductDetailViewModel
			{
				Id = productDto.Id,
				Name = productDto.Name,
				ProductCategoriesDto = _productCategoryService.GetProductCategoriesList(),
				ProductCategory = productDto.ProductCategoryId,
				AmountInStock = productDto.AmountInStock,
				Price = productDto.Price,
				Description = productDto.Description,
				DateOfAdding = productDto.DateOfAdding,
				ProductImagesDto = productDto.Images,
			};

			return viewModel;
		}

		public void AddProduct(ProductViewModel viewModel)
		{
			var product = new Product
			{
				ProductCategoryId = viewModel.ProductCategory,
				Name = viewModel.Name,
				AmountInStock = viewModel.AmountInStock,
				Price = viewModel.Price,
				Description = viewModel.Description,
				DateOfAdding = DateTime.Now,
			};

			_productRepository.Add(product);
			AddImagesToProduct(viewModel.Files, product);
			_productRepository.SaveChanges();
		}

		public void DeleteProduct(int id)
		{
			_productRepository.Delete(id);
			_productRepository.SaveChanges();
		}

		public bool ProductExist(int id)
		{
			return _productRepository.Exists(id);
		}

		public void EditProduct(ProductViewModel viewModel)
		{
			var product = _productRepository.Get(viewModel.Id);

			if (viewModel.Name != null)
			{
				product.Name = viewModel.Name;
				product.ProductCategoryId = viewModel.ProductCategory;
				product.AmountInStock = viewModel.AmountInStock;
				product.Price = viewModel.Price;
				product.Description = viewModel.Description;
			}

			AddImagesToProduct(viewModel.Files, product);

			_productRepository.SaveChanges();
		}

		public bool ProductPhotoExists(Guid id)
		{
			return _productRepository.PhotoExists(id);
		}

		public void DeletePhoto(Guid id)
		{
			List<string> photosNames = _productRepository.GetPhotosNames(id);

			if (System.IO.File.Exists(HostingEnvironment.MapPath("~/Images/") + photosNames[0]))
			{
				System.IO.File.Delete(HostingEnvironment.MapPath("~/Images/") + photosNames[0]);
			}

			if (System.IO.File.Exists(HostingEnvironment.MapPath("~/Images/") + photosNames[1]))
			{
				System.IO.File.Delete(HostingEnvironment.MapPath("~/Images/") + photosNames[1]);
			}

			_productRepository.DeletePhoto(id);
			_productRepository.SaveChanges();
		}

		public void AddImagesToProduct(IEnumerable<HttpPostedFileBase> files, Product product)
		{
			if (files != null)
			{
				var resizedImages = _productImageService.ResizeImages(files, 1920, 1080);
				var cropedImages = _productImageService.CropImage(files, 300, 200);

				for (int i = 0; i < files.Count(); i++)
				{
					product.Images = new List<ProductImage>();
					if (product.Images.Count <= 5)
					{
						Guid id = Guid.NewGuid();

						var productImage = new ProductImage
						{
							Id = id,
							ImageName = product.Name + "Image" + id + ".png",
							ThumbnailName = product.Name + "Thumbnail" + id + ".png",
						};

						product.Images.Add(productImage);

						using (resizedImages[i])
						using (cropedImages[i])
						{
							resizedImages[i].Save(HostingEnvironment.MapPath("~/Images/") + product.Name + "Image" + id + ".png");
							cropedImages[i].Save(HostingEnvironment.MapPath("~/Images/") + product.Name + "Thumbnail" + id + ".png");
						}
					}
				}
			}
		}
	}
}