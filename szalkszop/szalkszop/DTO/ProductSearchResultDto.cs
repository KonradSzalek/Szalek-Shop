﻿using System;

namespace szalkszop.ViewModels
{
	public class ProductSearchResultDto
	{
		public int Id { get; set; }

		public DateTime? DateOfAdding { get; set; }

		public int ProductCategoryId { get; set; }

		public string Name { get; set; }

		public int AmountInStock { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public string CategoryName { get; set; }

		public Guid? imageid { get; set; }

		public string ImageName { get; set; }

		public string ThumbnailName { get; set; }
	}
}