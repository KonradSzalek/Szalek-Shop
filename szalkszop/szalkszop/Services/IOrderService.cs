using System.Collections.Generic;
using szalkszop.DTO;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.Order;

namespace szalkszop.Services
{
	public interface IOrderService
	{
		void CompleteOrder(CreateOrderViewModel viewModel, string userId);
		IEnumerable<OrderDto> GetUserOrderList(string userId);
		IEnumerable<OrderDto> GetOrderList();
		IEnumerable<OrderItemDto> GetOrderItemList(int orderId);
		void UpdateStatus(int orderId, OrderStatus? status);
	}
}
