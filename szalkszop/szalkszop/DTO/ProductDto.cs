using System;
using System.Collections.Generic;

namespace szalkszop.DTO
{
	public class ProductDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime DateOfAdding { get; set; }

		public ProductCategoryDto ProductCategory { get; set; }

		public int ProductCategoryId { get; set; }

		public int? AmountInStock { get; set; }

		public double? Price { get; set; }

		public string Description { get; set; }

		public List<ProductImageDto> Images { get; set; }
	}
}