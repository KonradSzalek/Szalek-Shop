using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using szalkszop.Areas.Admin.ViewModels;
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

		public void AddImagesToProduct(IEnumerable<HttpPostedFileBase> files, Product product)
		{
			if (files.Any(x => x != null))
			{
				var resizedImages = _productImageService.ResizeImages(files, 1920, 1080);
				var cropedImages = _productImageService.CropImage(files, 300, 200);

				for (int i = 0; i < files.Count(); i++)
				{
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

		public void RemoveFromStock(int productId, int quantity)
		{
			var product = _productRepository.Get(productId);
			product.AmountInStock -= quantity;

			_productRepository.SaveChanges();
		}

		public void AddToStock(int productId, int quantity)
		{
			var product = _productRepository.Get(productId);
			product.AmountInStock += quantity;

			_productRepository.SaveChanges();
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

		public List<Item> ValidateStockAmounts(List<Item> orderedItemList)
		{
			foreach (var item in orderedItemList)
			{
				var productStockAmount = _productRepository.GetStockAmount(item.Product.Id);
				if (item.Quantity > productStockAmount)
				{
					item.Quantity = (int) productStockAmount;
				}
			}
			return orderedItemList;
		}

		public ProductListViewModel GetProductListByCategory(int categoryId)
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
				.Include(i => i.Images)
				.Where(p => p.ProductCategoryId == categoryId)
				.ToList();

			var productsDto = ProductMapper.MapToDto(products);

			var viewModel = new ProductListViewModel
			{
				ProductList = productsDto,
			};

			return viewModel;
		}

		public ProductFiltersViewModel GetProductSearch()
		{
			var viewModel = new ProductFiltersViewModel
			{
				ProductCategories = _productCategoryService.GetProductCategoryList(),
			};

			return viewModel;
		}

		public IEnumerable<ProductSearchResultDto> GetQueriedProductList(ProductFiltersViewModel searchModel)
		{
			IEnumerable<ProductSearchResultDto> products;

			return products = _productRepository.SearchResultFromSqlStoredProcedure(searchModel.Name,
				searchModel.PriceFrom,
				searchModel.PriceTo,
				searchModel.DateTimeTo,
				searchModel.DateTimeFrom,
				searchModel.ProductCategory.Id);
		}

		public IEnumerable<ProductDto> GetProductList()
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
				.Include(i => i.Images).ToList();

			var productsDto = ProductMapper.MapToDto(products);

			return productsDto;
		}

		public ProductViewModel AddProduct()
		{
			var viewModel = new ProductViewModel
			{
				ProductCategoryList = _productCategoryService.GetProductCategoryList(),
			};

			return viewModel;
		}

		public ProductViewModel EditProduct(int id)
		{
			var product = _productRepository.Get(id);

			var productDto = ProductMapper.MapToDto(product);

			var viewModel = new ProductViewModel
			{
				Id = productDto.Id,
				Name = productDto.Name,
				ProductCategoryList = _productCategoryService.GetProductCategoryList(),
				ProductCategory = productDto.ProductCategoryId,
				AmountInStock = productDto.AmountInStock,
				Price = productDto.Price,
				Description = productDto.Description,
				ProductImagesDto = productDto.Images,
			};

			return viewModel;
		}

		public ProductDetailViewModel GetProductDetail(int id)
		{
			var product = _productRepository.Get(id);

			var productDto = ProductMapper.MapToDto(product);

			var viewModel = new ProductDetailViewModel
			{
				Id = productDto.Id,
				Name = productDto.Name,
				ProductCategoryList = _productCategoryService.GetProductCategoryList(),
				ProductCategory = productDto.ProductCategoryId,
				AmountInStock = productDto.AmountInStock,
				Price = productDto.Price,
				Description = productDto.Description,
				DateOfAdding = productDto.DateOfAdding,
				ProductImageList = productDto.Images,
			};

			return viewModel;
		}

		public List<ProductImageDto> GetProductImages(int id)
		{
			var product = _productRepository.Get(id);

			var productDto = ProductMapper.MapToDto(product);

			return productDto.Images;
		}

		public ProductDto GetProduct(int id)
		{
			var product = _productRepository.Get(id);
			return ProductMapper.MapToDto(product);
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
				Images = new List<ProductImage>(),
			};
			_productRepository.Add(product);
			AddImagesToProduct(viewModel.Files, product);
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

			AddImagesToProduct(viewModel.Files, product);
			_productRepository.SaveChanges();
		}

		public void DeleteProduct(int id)
		{
			_productRepository.Delete(id);
			_productRepository.SaveChanges();
		}

		public bool DoesProductExist(int id)
		{
			return _productRepository.Exists(id);
		}

		public bool DoesProductPhotoExist(Guid id)
		{
			
			return _productRepository.PhotoExists(id);
		}

		public bool IsPhotoCountExceeded(int id, int filesCount)
		{
			bool isPhotosCountExceeded = false;
			var product = _productRepository.Get(id);

			if ((filesCount + product.Images.Count()) > 5)
			{
				isPhotosCountExceeded = true;
				return isPhotosCountExceeded;
			}

			return isPhotosCountExceeded;
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

		public int GetProductCount()
		{
			return _productRepository.GetProductCount();
		}
	}
}