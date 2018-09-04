using System;
using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class Product
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		public DateTime DateOfAdding { get; set; }

		public ProductCategory ProductCategory { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int ProductCategoryId { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int AmountInStock { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public double Price { get; set; }

		[Required]
		[StringLength(255)]
		public string Description { get; set; }
	}
}