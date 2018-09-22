using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;

namespace szalkszop.Repositories
{
	public class ProductCategoryMapper : IProductCategoryMapper
	{
		public IEnumerable<ProductCategoryDto> MapToDto(IEnumerable<ProductCategory> categories)
		{
			return categories.Select(category => MapToDto(category));
		}

		public ProductCategoryDto MapToDto(ProductCategory productCategory)
		{
			var productCategoryDto = new ProductCategoryDto
			{
				Id = productCategory.Id,
				Name = productCategory.Name,
			};

			return productCategoryDto;
		}

		public IEnumerable<ProductCategorySearchResultDto> MapToDtoWithAmountOfProducts(IEnumerable<Product> products, IEnumerable<ProductCategory> categories)
		{
			var categoriesDto = categories.Select(category => new ProductCategorySearchResultDto()
			{
				Id = category.Id,
				Name = category.Name,
				AmountOfProducts = products.Count(p => p.ProductCategoryId == category.Id)
			});

			return categoriesDto;
		}
	}
}