using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class OrderListViewModel
	{
		public List<OrderDto> Orders { get; set; }
		public List<OrderItemDto> OrdersItems { get; set; }
	}
}