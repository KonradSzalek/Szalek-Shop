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

		public IEnumerable<ProductCategoryWithProductCountDto> GetCategoriesWithoutEmptyCategories()
		{
			var products = _productRepository.GetList();
			var categories = _productCategoryRepository.GetList();

			return ProductCategoryMapper.MapToDtoWithoutEmptyCategories(products, categories);
		}

		public void AddProductCategory(AdminProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_productCategoryRepository.Add(category);
			_productCategoryRepository.SaveChanges();
		}

		public void EditProductCategory(AdminProductCategoryViewModel viewModel)
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
	
		public AdminProductCategoriesViewModel GetAdminProductCategoriesViewModel()
		{
			var viewModel = new AdminProductCategoriesViewModel
			{
				ProductCategoriesWithProductCountDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}
	
		public UserProductCategoriesViewModel GetUserProductCategoriesViewModel()
		{
			var viewModel = new UserProductCategoriesViewModel
			{
				ProductCategoriesWithProductCountDto = GetCategoriesWithoutEmptyCategories(),
			};

			return viewModel;
		}
	
		public AdminProductCategoryViewModel EditProductCategoryViewModel(int id)
		{
			var productCategory = _productCategoryRepository.Get(id);

			var viewModel = new AdminProductCategoryViewModel
			{
				Name = productCategory.Name,
				Id = productCategory.Id,
			};

			return viewModel;
		}
	}
}