using System;
using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class ProductImage
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(255)]
		public string ImageName { get; set; }

		[Required]
		[StringLength(255)]
		public string ThumbnailName { get; set; }
	}
}