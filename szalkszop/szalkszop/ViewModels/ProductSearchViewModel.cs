using System;
using System.Collections.Generic;
using System.ComponentModel;
using szalkszop.Core.Models;
using szalkszop.DTO;
using System.ComponentModel.DataAnnotations;

namespace szalkszop.ViewModels
{
	public class ProductSearchViewModel
	{
		// Walidacja nie działa, nie wiem czemu
        // cr4 szukanie po Id mozesz olac
		public int? Id { get; set; }

		[StringLength(100)]
		public string Name { get; set; }

		[DisplayName("Price from")]
		[Range(0, int.MaxValue)]
		public int? PriceFrom { get; set; }

		[DisplayName("Price to")]
		[Range(0, int.MaxValue)]
		public int? PriceTo { get; set; }

		[DisplayName("Date from")]
		[DataType(DataType.DateTime)]
		public DateTime? DateTimeFrom { get; set; }

		[DisplayName("Date to")]
		[DataType(DataType.DateTime)]
		public DateTime? DateTimeTo { get; set; }

		[DisplayName("Product Category")]
		public ProductCategoryDto ProductCategory { get; set; }

		public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
	}
}