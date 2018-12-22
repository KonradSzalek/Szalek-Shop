using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductListViewModel
	{
		public IEnumerable<ProductDto> ProductList { get; set; }
	}
}