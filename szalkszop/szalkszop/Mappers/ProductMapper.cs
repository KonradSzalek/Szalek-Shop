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
				}
			});
		}

		public ProductDto MapToDto(Product product)
		{
			var productDto = new ProductDto()
			{
				Id = product.Id,
				Name = product.Name,
				DateOfAdding = product.DateOfAdding,
				AmountInStock = product.AmountInStock,
				Price = product.Price,
				Description = product.Description,
				ProductCategoryId = product.ProductCategoryId,
				ProductCategory = new ProductCategoryDto
				{
					Id = product.ProductCategory.Id,
					Name = product.ProductCategory.Name
				}
			};
			return productDto;
		}
	}
}