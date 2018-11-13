using System;
using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class ProductImage
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(255)]
		public string FileName { get; set; }
	}
}