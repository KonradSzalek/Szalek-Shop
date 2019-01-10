using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductListSearchViewModel
	{
		public IEnumerable<ProductSearchResultDto> ProductSearchResultList { get; set; }
		public IEnumerable<ProductDto> ProductList { get; set; }
		public ProductFiltersViewModel ProductFiltersViewModel { get; set; }
	}
}