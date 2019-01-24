﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace szalkszop.Core.Models
{
	public class ProductCategory
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public bool IsActive { get; set; } = true;
	}
}