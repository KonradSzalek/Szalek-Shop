using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class UserProductCategoriesViewModel
	{
		public IEnumerable<ProductCategoryWithProductCountDto> ProductCategoriesWithProductCountDto { get; set; }
	}
}