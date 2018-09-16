using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public class ProductCategoryMapper
	{
		public IEnumerable<ProductCategoryDto> MapToDto(List<ProductCategory> categories)
		{
			return categories.Select(category => new ProductCategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
			});
		}

		public ProductCategoryDto MapToDto(ProductCategory productCategory)
		{
			return new ProductCategoryDto
			{
				Id = productCategory.Id,
				Name = productCategory.Name,
			};
		}

		public IEnumerable<ProductCategoryDto> MapToDtoWithAmountOfProducts(List<ProductDto> products, IEnumerable<ProductCategory> categories)
		{
			var categoriesDto = categories.Select(category => new ProductCategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				AmountOfProducts = products.Count(p => p.ProductCategory.Id == category.Id)
			});

			return categoriesDto;
		}
	}
}