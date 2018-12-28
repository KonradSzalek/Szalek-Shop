using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class OrderItemDto
	{
		public int OrderId { get; set; }
		public string Name { get; set; }
		public string CategoryName { get; set; }
		public int Quantity { get; set; }
		public double? Price { get; set; }
	}
}