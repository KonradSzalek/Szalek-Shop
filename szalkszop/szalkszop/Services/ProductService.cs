using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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

		public ProductService(IProductRepository productRepository, IProductCategoryService productCategoryRepository)
		{
			_productRepository = productRepository;
			_productCategoryService = productCategoryRepository;
		}

		private IEnumerable<Product> GetProductsWithCategory()
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory);

			return products;
		}

		private IEnumerable<Product> GetQueriedProducts(ProductSearchViewModel searchModel)
		{
			var products = GetProductsWithCategory();

			if (searchModel != null)
			{
				if (!string.IsNullOrEmpty(searchModel.Name))
					products = products.Where(p => p.Name.Contains(searchModel.Name));
				if (searchModel.PriceFrom.HasValue)
					products = products.Where(p => p.Price >= searchModel.PriceFrom);
				if (searchModel.PriceTo.HasValue)
					products = products.Where(p => p.Price <= searchModel.PriceTo);
				if (searchModel.DateTimeFrom.HasValue)
					products = products.Where(p => p.DateOfAdding >= searchModel.DateTimeFrom);
				if (searchModel.DateTimeTo.HasValue)
					products = products.Where(p => p.DateOfAdding <= searchModel.DateTimeTo);
				if (searchModel.ProductCategory.Id != 0)
					products = products.Where(p => p.ProductCategory.Id == searchModel.ProductCategory.Id);
			}

			return products;
		}

		public IEnumerable<ProductDto> GetThreeNewestProducts()
		{
			var products = _productRepository.GetList()
				.Include(p => p.ProductCategory)
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

		public IEnumerable<ProductDto> GetQueriedProductSearch(ProductSearchViewModel searchModel)
		{
			var products = GetQueriedProducts(searchModel);

			var productsDto = ProductMapper.MapToDto(products);

			return productsDto;
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

		public void DeleteProductPhotos(int id, bool? imageUploaded1, bool? imageUploaded2, bool? imageUploaded3)
		{
			var product = _productRepository.Get(id);

			if (imageUploaded1 != null)
			{
				product.ImageUploaded1 = imageUploaded1;
			}

			if (imageUploaded2 != null)
			{
				product.ImageUploaded2 = imageUploaded2;
			}

			if (imageUploaded3 != null)
			{
				product.ImageUploaded3 = imageUploaded3;
			}

			_productRepository.SaveChanges();
		}

		public void EditProduct(ProductViewModel viewModel, bool? imageUploaded1, bool? imageUploaded2, bool? imageUploaded3)
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

			if (imageUploaded1 != null)
			{
				product.ImageUploaded1 = imageUploaded1;
			}

			if (imageUploaded2 != null)
			{
				product.ImageUploaded2 = imageUploaded2;
			}

			if (imageUploaded3 != null)
			{
				product.ImageUploaded3 = imageUploaded3;
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