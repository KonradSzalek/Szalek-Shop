using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
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

		private IEnumerable<Product> GetProductsWithCategory()
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
				.Include(i => i.Images);

			return products;
		}

		public IEnumerable<ProductSearchResult> GetQueriedProducts(ProductSearchViewModel searchModel)
		{
			IEnumerable<ProductSearchResult> products;

			return products = _productRepository.SearchResultFromSqlStoredProcedure(searchModel.Name,
				searchModel.PriceFrom,
				searchModel.PriceTo,
				searchModel.DateTimeFrom,
				searchModel.DateTimeFrom,
				searchModel.ProductCategory.Id);
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

		public ProductSearchViewModel GetProductSearchViewModel()
		{
			var viewModel = new ProductSearchViewModel
			{
				ProductCategories = _productCategoryService.GetProductCategoriesList(),
			};

			return viewModel;
		}

		public IEnumerable<ProductDto> GetProducts()
		{
			var products = GetProductsWithCategory().ToList();

			var productsDto = ProductMapper.MapToDto(products);

			return productsDto;
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

		public int AddProduct(ProductViewModel viewModel)
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
			_productRepository.SaveChanges();

			return product.Id;
		}

		public void DeletePhoto(Guid id, int productId)
		{
			var product = _productRepository.Get(productId);
			var image = product.Images.Single(i => i.Id == id);

			if (System.IO.File.Exists(HostingEnvironment.MapPath("~/Images/") + image.ImageName))
			{
				System.IO.File.Delete(HostingEnvironment.MapPath("~/Images/") + image.ImageName);
			}

			if (System.IO.File.Exists(HostingEnvironment.MapPath("~/Images/") + image.ThumbnailName))
			{
				System.IO.File.Delete(HostingEnvironment.MapPath("~/Images/") + image.ThumbnailName);
			}

			product.Images.Remove(image);

			_productRepository.SaveChanges();
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

			if (viewModel.Files != null)
			{
				var images = _productImageService.GetFromFiles(viewModel.Files);
				var resizedImages = _productImageService.ResizeImages(images, 1920, 1080);
				var thumbnailImages = _productImageService.ThumbnailImages(resizedImages);
				int i = 0;

				foreach (var image in images)
				{
					Guid id = Guid.NewGuid();

					var productImage = new ProductImage
					{
						Id = id,
						ImageName = viewModel.Name + viewModel.Id + "Image" + id + ".png",
						ThumbnailName = viewModel.Name + viewModel.Id + "Thumbnail" + id + ".png",
					};
					product.Images.Add(productImage);
					resizedImages[i].Save(HostingEnvironment.MapPath("~/Images/") + viewModel.Name + viewModel.Id + "Image" + id + ".png");
					thumbnailImages[i].Save(HostingEnvironment.MapPath("~/Images/") + viewModel.Name + viewModel.Id + "Thumbnail" + id + ".png");
					i++;
				}
			}
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
	}
}