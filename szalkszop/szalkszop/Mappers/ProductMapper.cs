using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public static class ProductMapper
	{
		public static IEnumerable<ProductDto> MapToDto(IEnumerable<Product> products)
		{
			return products.Select(n => MapToDto(n));
		}

		public static ProductDto MapToDto(Product product)
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
				Images = product.Images.Select(i => new ProductImageDto()
				{
					Id = i.Id,
					ImageName = i.ImageName,
					ThumbNailName = i.ThumbnailName

				}).ToList(),
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