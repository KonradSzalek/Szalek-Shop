using System.Collections.Generic;
using szalkszop.DTO;
using static szalkszop.Core.Models.Order;

namespace szalkszop.Areas.Admin.ViewModels
{
	public class AdminOrderViewModel
	{
		public IEnumerable<OrderDto> Orders { get; set; }
		public int OrderId { get; set; }
		public OrderStatus? Status { get; set; }
	}
}
