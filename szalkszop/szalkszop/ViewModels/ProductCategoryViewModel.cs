using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductCategoryViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public string Heading { get; set; }

		public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
	}
}