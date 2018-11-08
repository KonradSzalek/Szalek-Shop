using System;
using szalkszop.Repositories;

namespace szalkszop.DTO
{
	public class ProductDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime DateOfAdding { get; set; }

		public ProductCategoryDto ProductCategory { get; set; }

		public int ProductCategoryId { get; set; }

		public int AmountInStock { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public bool? ImageUploaded1 { get; set; }

		public bool? ImageUploaded2 { get; set; }

		public bool? ImageUploaded3 { get; set; }
	}
}