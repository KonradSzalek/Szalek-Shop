using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class ApiCartProductDto
	{
		public string RemoveLink { get; set; }
		public string RemoveSingleLink { get; set; }
		public string BuyLink { get; set; }
		public string Thumbnail { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public string Name { get; set; }
		public int ProductId { get; set; }
	}
}