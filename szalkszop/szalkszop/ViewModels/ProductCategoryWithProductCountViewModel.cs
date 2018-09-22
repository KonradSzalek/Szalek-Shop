using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductCategoriesWithProductCountViewModel
	{
		public IEnumerable<ProductCategoryWithProductCountDto> ProductCategoriesWithProductCountDto { get; set; } 
	}
}