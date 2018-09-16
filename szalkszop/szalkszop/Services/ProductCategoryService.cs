using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;

namespace szalkszop.Services
{
	public class ProductCategoryService
	{
		private readonly ProductCategoryRepository _productCategoryRepository;
		private readonly ProductCategoryMapper _productCategoryMapper;

		public ProductCategoryService(ProductCategoryRepository productCategoryRepository, ProductCategoryMapper productCategoryMapper)
		{
			_productCategoryRepository = productCategoryRepository;
			_productCategoryMapper = productCategoryMapper;
		}

		public IEnumerable<ProductCategoryDto> GetProductCategories()
		{
			var categories = _productCategoryRepository.GetProductCategoryList().ToList();

			return _productCategoryMapper.MapToDto(categories);
		}

		public ProductCategoryDto GetEditingProductCategoryDto(int id)
		{
			var productCategory = _productCategoryRepository.GetProductCategory(id);

			return _productCategoryMapper.MapToDto(productCategory);
		}

		public ProductCategory GetEditingProductCategory(int id)
		{
			return _productCategoryRepository.GetProductCategory(id);
		}

		public IEnumerable<ProductCategoryDto> GetCategoriesWithAmountOfProducts(List<ProductDto> products)
		{
			var categories = _productCategoryRepository.GetProductCategoryList();

			return _productCategoryMapper.MapToDtoWithAmountOfProducts(products, categories);
		}

		public void Add(ProductCategory category)
		{
			_productCategoryRepository.Add((category));
		}

		public void Remove(ProductCategory category)
		{
			_productCategoryRepository.Remove(category);
		}

		public void Complete()
		{
			_productCategoryRepository.Complete();
		}
	}
}