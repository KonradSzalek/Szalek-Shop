using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductRepository _productRepository;

		public ProductCategoryService(ProductCategoryRepository productCategoryRepository, ProductRepository productRepository)
		{
			_productCategoryRepository = productCategoryRepository;
			_productRepository = productRepository;
		}

		public IEnumerable<ProductCategoryDto> GetProductCategoriesList()
		{
			var productCategories = _productCategoryRepository.GetList();

			return ProductCategoryMapper.MapToDto(productCategories);
		}

		public IEnumerable<ProductCategoryWithProductCountDto> GetCategoriesWithAmountOfProducts()
		{
			var products = _productRepository.GetList();
			var categories = _productCategoryRepository.GetList();

			return ProductCategoryMapper.MapToDtoWithAmountOfProducts(products, categories);
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
			var category = _productCategoryRepository.Get(viewModel.Id);

			category.Name = viewModel.Name;

			_productCategoryRepository.SaveChanges();
		}

		public void DeleteProductCategory(int id)
		{
			_productCategoryRepository.Delete(id);
			_productCategoryRepository.SaveChanges();
		}

		public bool ProductCategoryExist(int id)
		{
			return _productCategoryRepository.Exists(id);
		}


		public ProductCategoriesWithProductCountViewModel GetProductCategoriesWithProductCountViewModel()
		{
			var viewModel = new ProductCategoriesWithProductCountViewModel
			{
				ProductCategoriesWithProductCountDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoriesViewModel GetProductCategoriesViewModel()
		{
			var productCategories = _productCategoryRepository.GetList();
			var productCategoriesDto = ProductCategoryMapper.MapToDto(productCategories);

			var viewModel = new ProductCategoriesViewModel
			{
				ProductCategoriesDto = productCategoriesDto,
			};

			return viewModel;
		}

		public ProductCategoryViewModel EditProductCategoryViewModel(int id)
		{
			var productCategory = _productCategoryRepository.Get(id);

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Update Category",
				Name = productCategory.Name,
				Id = productCategory.Id,
			};

			return viewModel;
		}
	}
}