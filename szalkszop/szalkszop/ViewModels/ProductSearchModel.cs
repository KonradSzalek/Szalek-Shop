using System;
using System.Collections.Generic;
using System.ComponentModel;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductSearchModel
	{
		public int? Id { get; set; }
		public string Name { get; set; }

		[DisplayName("Price from")]
		public int? PriceFrom { get; set; }

		[DisplayName("Price to")]
		public int? PriceTo { get; set; }

		[DisplayName("Date from")]
		public DateTime? DateTimeFrom { get; set; }

		[DisplayName("Date to")]
		public DateTime? DateTimeTo { get; set; }

		[DisplayName("Product Category")]
		public ProductCategory ProductCategory { get; set; }
		public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
	}
}