using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class UserProductCategoryListViewModel
	{
		public IEnumerable<ProductCategoryWithProductCountDto> ProductCategoryWithProductCountList { get; set; }
	}
}