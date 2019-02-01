using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class MyOrdersViewModel
	{
		public IEnumerable<OrderDto> Orders { get; set; }
		public bool OrderMade { get; set; }
	}
}