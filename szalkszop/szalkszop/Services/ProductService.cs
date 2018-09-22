using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductMapper _productMapper;

		public ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, ProductMapper productMapper)
		{
			_productRepository = productRepository;
			_productCategoryRepository = productCategoryRepository;
			_productMapper = productMapper;
		}

		public ProductDto GetEditingProductDto(int id)
		{
			var product = _productRepository.GetProduct(id);

			return _productMapper.MapToDto(product);
		}

		public Product GetEditingProduct(int id)
		{
			var product = _productRepository.GetProduct(id);

			return product;
		}

		public IEnumerable<Product> GetProductsWithCategory()
		{
			var products = _productRepository.GetProductList()
				.Include(p => p.ProductCategory)
				.ToList();

			return products;
		}

		public IEnumerable<Product> GetProductInCategory(int id)
		{
			var products = _productRepository.GetProductList()
				.Include(p => p.ProductCategory)
				.Where(p => p.ProductCategoryId == id)
				.ToList();

			return products;
		}

		public IEnumerable<Product> GetQueriedProducts(ProductSearchModel searchModel, IEnumerable<Product> products)
		{
			if (searchModel != null)
			{
				if (searchModel.Id.HasValue)
					products = products.Where(p => p.Id == searchModel.Id);
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

		public ProductViewModel GetThreeNewestProductsViewModel()
		{
			var products = _productRepository.GetProductList()
				.Include(p => p.ProductCategory)
				.OrderByDescending(d => d.DateOfAdding)
				.Take(3)
				.ToList();

			var viewModel = new ProductViewModel
			{
				Products = products,
			};

			return viewModel;
		}

		public ProductViewModel GetShowProductInCategoryViewModel(int id)
		{
			var viewModel = new ProductViewModel
			{
				Products = GetProductInCategory(id)
			};
			return viewModel;
		}

		public ProductSearchModel GetProductSearchViewModel()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _productCategoryRepository.GetProductCategoryList(),
			};

			return viewModel;
		}

		public ProductViewModel GetQueriedProductSearchViewModel(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = GetQueriedProducts(searchModel, GetProductsWithCategory()),
			};

			return viewModel;
		}

		public ProductViewModel GetProductViewModel()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = GetProductsWithCategory(),
			};

			return viewModel;
		}

		public ProductViewModel AddProductViewModel()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Add a product",
				ProductCategories = _productCategoryRepository.GetProductCategoryList(),
			};

			return viewModel;
		}

		public ProductViewModel EditProductViewModel(int id)
		{
			var product = GetEditingProductDto(id);

			var viewModel = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				ProductCategories = _productCategoryRepository.GetProductCategoryList(),
				ProductCategory = product.ProductCategoryId,
				AmountInStock = product.AmountInStock,
				Price = product.Price,
				Description = product.Description,
				Heading = "Edit a product",
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
			_productRepository.SaveChanges();
		}

		public void EditProduct(ProductViewModel viewModel)
		{
			var product = GetEditingProduct(viewModel.Id);

			product.Name = viewModel.Name;
			product.ProductCategoryId = viewModel.ProductCategory;
			product.AmountInStock = viewModel.AmountInStock;
			product.Price = viewModel.Price;
			product.Description = viewModel.Description;

			_productRepository.SaveChanges();
		}

		public void DeleteProduct(int id)
		{
			_productRepository.Remove(id);
			_productRepository.SaveChanges();
		}

		public bool IsProductExist(int id)
		{
			return _productRepository.IsProductExist(id);
		}
	}
}