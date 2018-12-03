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

		public IEnumerable<ProductCategoryWithProductCountDto> GetCategoriesWithAmountOfProductsWithoutEmpty()
		{
			var products = _productRepository.GetList();
			var categories = _productCategoryRepository.GetList();

			return ProductCategoryMapper.MapToDtoWithAmountOfProductsWithoutEmptyProducts(products, categories);
		}
		//sprawdzone
		public void AddProductCategory(ProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_productCategoryRepository.Add(category);
			_productCategoryRepository.SaveChanges();
		}
		//sprawdzone
		public void EditProductCategory(ProductCategoryViewModel viewModel)
		{
			var category = _productCategoryRepository.Get(viewModel.Id);

			category.Name = viewModel.Name;

			_productCategoryRepository.SaveChanges();
		}
		//sprawdzone
		public void DeleteProductCategory(int id)
		{
			_productCategoryRepository.Delete(id);
			_productCategoryRepository.SaveChanges();
		}
		//sprawdzone
		public bool ProductCategoryExist(int id)
		{
			return _productCategoryRepository.Exists(id);
		}
		//sprawdzone
		public AdminProductCategoriesViewModel GetAdminProductCategoriesViewModel()
		{
			var viewModel = new AdminProductCategoriesViewModel
			{
				ProductCategoriesWithProductCountDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}
		//sprawdzone
		public UserProductCategoriesViewModel GetUserProductCategoriesViewModel()
		{
			var viewModel = new UserProductCategoriesViewModel
			{
				ProductCategoriesWithProductCountDto = GetCategoriesWithAmountOfProductsWithoutEmpty(),
			};

			return viewModel;
		}
		//sprawdzone
		public UserProductCategoriesViewModel GetProductCategoriesViewModelWithoutEmpty()
		{
			var products = _productRepository.GetList();
			var productCategories = _productCategoryRepository.GetList();
			var productCategoriesDto = ProductCategoryMapper.MapToDtoWithAmountOfProductsWithoutEmptyProducts(products, productCategories);

			var viewModel = new UserProductCategoriesViewModel
			{
				ProductCategoriesWithProductCountDto = productCategoriesDto,
			};

			return viewModel;
		}

		public UserProductCategoriesViewModel GetProductCategoriesViewModel()
		{
			var products = _productRepository.GetList();
			var productCategories = _productCategoryRepository.GetList();
			var productCategoriesDto = ProductCategoryMapper.MapToDtoWithAmountOfProducts(products, productCategories);

			var viewModel = new UserProductCategoriesViewModel
			{
				ProductCategoriesWithProductCountDto = productCategoriesDto,
			};

			return viewModel;
		}

		public ProductCategoryViewModel EditProductCategoryViewModel(int id)
		{
			var productCategory = _productCategoryRepository.Get(id);

			var viewModel = new ProductCategoryViewModel
			{
				Name = productCategory.Name,
				Id = productCategory.Id,
			};

			return viewModel;
		}
	}
}