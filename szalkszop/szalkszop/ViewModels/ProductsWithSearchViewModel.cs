using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductsWithSearchViewModel
	{
        //CR5 nie powinienes potrzebowac dodawac nowego propa do search resultow
        //Po co ci one? Dlaczego nie mozesz wykorzystac Product DTO?
		public IEnumerable<ProductSearchResult> ProductSearchResult { get; set; }
		public IEnumerable<ProductDto> ProductsDto { get; set; }

        //CR5 po co ci ten pojedynczy prop?
		public ProductSearchViewModel ProductSearchViewModel { get; set; }
	}
}