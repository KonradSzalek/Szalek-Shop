using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductDetailViewModel
	{
		public int Id { get; set; }

		public DateTime DateOfAdding { get; set; }

		public string Name { get; set; }

		public int ProductCategory { get; set; }

		public int? AmountInStock { get; set; }

		public double? Price { get; set; }

		public string Description { get; set; }

		public IEnumerable<ProductCategoryDto> ProductCategoryList { get; set; }

		public List<ProductImageDto> ProductImageList { get; set; }
	}
}