using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{   //cr5 admin view model nie jest w admin area
	public class AdminProductCategoriesViewModel
	{
		public IEnumerable<ProductCategoryWithProductCountDto> ProductCategoriesWithProductCountDto { get; set; } 
	}
}