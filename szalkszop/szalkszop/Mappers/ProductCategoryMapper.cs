using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public static class ProductCategoryMapper
	{
		public static IEnumerable<ProductCategoryDto> MapToDto(IEnumerable<ProductCategory> categories)
		{
			return categories.Select(category => MapToDto(category));
		}

		public static ProductCategoryDto MapToDto(ProductCategory productCategory)
		{
			var productCategoryDto = new ProductCategoryDto
			{
				Id = productCategory.Id,
				Name = productCategory.Name,
			};

			return productCategoryDto;
		}

		public static IEnumerable<ProductCategoryWithProductCountDto> MapToDtoWithProductCount(IEnumerable<Product> products, IEnumerable<ProductCategory> categories)
		{
			var categoriesDto = categories.Select(category => new ProductCategoryWithProductCountDto()
			{
				Id = category.Id,
				Name = category.Name,
				AmountOfProducts = products.Count(p => p.ProductCategoryId == category.Id)
			});

			return categoriesDto;
		}
	}
}