using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;

namespace szalkszop.Repositories
{
	public class ProductMapper : IProductMapper
	{
		public IEnumerable<ProductDto> MapToDto(IEnumerable<Product> products)
		{
			return products.Select(n => MapToDto(n));
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