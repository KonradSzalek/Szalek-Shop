using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{   //CR5FIXED admin view model nie jest w admin area
	// - juz jest
	public class AdminProductCategoryListViewModel
	{
		public IEnumerable<ProductCategoryWithProductCountDto> ProductCategoryWithProductCountList { get; set; }
	}
}