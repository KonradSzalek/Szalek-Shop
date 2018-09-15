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
			return categories.Select(n => new ProductCategoryDto()
			{
				Id = n.Id,
				Name = n.Name,
				AmountOfProducts = n.AmountOfProducts,
			});
		}
	}
}