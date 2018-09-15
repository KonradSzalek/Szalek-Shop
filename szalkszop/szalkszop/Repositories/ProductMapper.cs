using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public class ProductMapper
	{
		public IEnumerable<ProductDto> MapToDto(List<Product> products)
		{
			return products.Select(n => new ProductDto()
			{
				Id = n.Id,
				Name = n.Name,
				DateOfAdding = n.DateOfAdding,
				AmountInStock = n.AmountInStock,
				Price = n.Price,
				Description = n.Description,
				ProductCategory = new ProductCategoryDto
				{
					Id = n.ProductCategory.Id,
					Name = n.ProductCategory.Name,
					AmountOfProducts = n.ProductCategory.AmountOfProducts,
				}
			});
		}
	}
}