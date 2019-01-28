using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductFiltersViewModel
	{
		[StringLength(100)]
		public string Name { get; set; }

		[DisplayName("Price from")]
		[Range(0, double.MaxValue)]
		public int? PriceFrom { get; set; }

		[DisplayName("Price to")]
		[Range(0, double.MaxValue)]
		public int? PriceTo { get; set; }

		[DisplayName("Date from")]
		[DataType(DataType.DateTime)]
		public DateTime? DateTimeFrom { get; set; }

		[DisplayName("Date to")]
		[DataType(DataType.DateTime)]
		public DateTime? DateTimeTo { get; set; }

		[DisplayName("Product Category")]
		public ProductCategoryDto ProductCategory { get; set; }

		public int ProductCategoryId { get; set; }

		public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
	}
}