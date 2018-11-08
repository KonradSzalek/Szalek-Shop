using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public int ProductCategoryId { get; set; }

		public int AmountInStock { get; set; }

		public double Price { get; set; }

		[Required]
		[StringLength(255)]
		public string Description { get; set; }

		public bool? ImageUploaded1 { get; set; }
		public bool? ImageUploaded2 { get; set; }
		public bool? ImageUploaded3 { get; set; }
	}
}