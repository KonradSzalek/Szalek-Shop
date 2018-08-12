using System;
using System.ComponentModel.DataAnnotations;

namespace szalkszop.Models
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
		public int ProductCategoryId { get; set; }
	}
}