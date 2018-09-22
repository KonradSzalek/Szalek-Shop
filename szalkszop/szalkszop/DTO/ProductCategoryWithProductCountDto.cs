using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class ProductCategoryWithProductCountDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int AmountOfProducts { get; set; }
	}
}