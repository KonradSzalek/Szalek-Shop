using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static szalkszop.Core.Models.Order;

namespace szalkszop.DTO
{
	public class OrderDto
	{
		public string ShippingAddress { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime OrderDate { get; set; }
	}
}