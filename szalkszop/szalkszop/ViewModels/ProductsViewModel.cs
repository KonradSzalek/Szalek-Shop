using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductsViewModel
	{
		public IEnumerable<ProductDto> ProductsDto { get; set; }
	}
}