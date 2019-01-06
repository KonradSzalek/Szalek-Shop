using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{   
	public class ProductCategoryListViewModel
	{
		public IEnumerable<ProductCategoryWithProductCountDto> ProductCategoryWithProductCountList { get; set; }
	}
}