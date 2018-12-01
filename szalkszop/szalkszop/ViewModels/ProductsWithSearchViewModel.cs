using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductsWithSearchViewModel
	{
		public IEnumerable<ProductSearchResult> ProductSearchResult { get; set; }
		public IEnumerable<ProductDto> ProductsDto { get; set; }
		public ProductSearchViewModel ProductSearchViewModel { get; set; }
	}
}