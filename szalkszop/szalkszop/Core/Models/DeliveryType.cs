﻿using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class DeliveryType
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public double? Cost { get; set; }
	}
}