using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductRepository _productRepository;
		private readonly IProductCategoryMapper _productCategoryMapper;

		public ProductCategoryService(ProductCategoryRepository productCategoryRepository, ProductCategoryMapper productCategoryMapper, ProductRepository productRepository)
		{
			_productCategoryRepository = productCategoryRepository;
			_productCategoryMapper = productCategoryMapper;
			_productRepository = productRepository;
		}

		public IEnumerable<ProductCategory> GetProductCategories()
		{
			var categories = _productCategoryRepository.GetProductCategoryList().ToList();

			return categories;
		}

		public ProductCategoryDto GetEditingProductCategoryDto(int id)
		{
			var productCategory = _productCategoryRepository.GetProductCategory(id);

			return _productCategoryMapper.MapToDto(productCategory);
		}

		public IEnumerable<ProductCategorySearchResultDto> GetCategoriesWithAmountOfProducts()
		{		
			var products = _productRepository.GetProductList();
			var categories = _productCategoryRepository.GetProductCategoryList();

			// cr2 to sie da zrobic jeszcze lepiej bez udzialu mappera
			// trzeba uzyc linq query syntax i zrobic joina jak na bazie danych
			// obawiam sie ze bez wczesniejszej zabawy z baza danych tego nie ogarniesz wiec poki co zobacz sobie w googlach
			// Linq query syntax
			return _productCategoryMapper.MapToDtoWithAmountOfProducts(products, categories);
		}

		public void AddProductCategory(ProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};
			_productCategoryRepository.Add(category);
			_productCategoryRepository.SaveChanges();
		}

		public void EditProductCategory(ProductCategoryViewModel viewModel)
		{
			var category = _productCategoryRepository.GetProductCategory(viewModel.Id);

			category.Name = viewModel.Name;

			_productCategoryRepository.SaveChanges();
		}

		public void DeleteProductCategory(int id)
		{
			_productCategoryRepository.Remove(id);
			_productCategoryRepository.SaveChanges();
		}

		public bool IsProductCategoryExist(int id)
		{
			return _productCategoryRepository.IsCategoryExist(id);
		}


		public ProductCategoryViewModel GetPartialCategoryView()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategoriesSearchResultDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoryViewModel GetProductCategorySearchResultViewModel()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategoriesSearchResultDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoryViewModel GetProductCategoryViewModel()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategoriesSearchResultDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoryViewModel AddProductCategoryViewModel()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Add a new category",
			};

			return viewModel;
		}

		public ProductCategoryViewModel EditProductCategoryViewModel(int id)
		{
			var category = GetEditingProductCategoryDto(id);

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Update Category",
				Name = category.Name,
			};

			return viewModel;
		}
	}
}