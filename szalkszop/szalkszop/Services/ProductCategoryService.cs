using System.Collections.Generic;
using System.Linq;
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

		public IEnumerable<ProductCategoryDto> GetProductCategoryList()
		{
			var productCategories = _productCategoryRepository.GetList();

			return ProductCategoryMapper.MapToDto(productCategories);
		}

		public IEnumerable<ProductCategoryWithProductCountDto> GetProductCategoryWithProductCountList()
		{
			var products = _productRepository.GetList();
			var categories = _productCategoryRepository.GetList();

			var productCategoryList = ProductCategoryMapper.MapToDtoWithProductCount(products, categories);	

			return productCategoryList;
		}

		public UserProductCategoryListViewModel GetPopulatedOnlyProductCategoryList()
		{
			var products = _productRepository.GetList();
			var categories = _productCategoryRepository.GetList();

			var viewModel = new UserProductCategoryListViewModel
			{
				ProductCategoryWithProductCountList = ProductCategoryMapper.MapToDtoWithProductCount(products, categories)
				.Where(p => p.AmountOfProducts > 0),
			};

			return viewModel;
		}

		public ProductCategoryViewModel EditProductCategory(int id)
		{
			var productCategory = _productCategoryRepository.Get(id);

			var viewModel = new ProductCategoryViewModel
			{
				Name = productCategory.Name,
				Id = productCategory.Id,
			};

			return viewModel;
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

		public bool DoesProductCategoryExist(int id)
		{
			return _productCategoryRepository.Exists(id);
		}

		public int GetProductCategoryCount()
		{
			return _productRepository.GetProductCount();
		}
	}
}