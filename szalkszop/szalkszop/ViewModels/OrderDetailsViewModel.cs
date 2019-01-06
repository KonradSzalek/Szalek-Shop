using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class OrderDetailsViewModel
	{
		public IEnumerable<OrderItemDto> OrderItems { get; set; }
		public OrderDto Order { get; set; }
	}
}