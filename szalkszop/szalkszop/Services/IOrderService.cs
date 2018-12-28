using System.Collections.Generic;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IOrderService
	{
		void AddPaymentMethod();
		void AddDeliveryType();
		void DeletePaymentMethod();
		void DeleteDeliveryType();
		void CompleteOrder(OrderViewModel viewModel, string userId);
		IEnumerable<PaymentMethodDto> GetPaymentMethodList();
		IEnumerable<DeliveryTypeDto> GetDeliveryTypeList();
		IEnumerable<OrderDto> GetUserOrderList(string userId);
		IEnumerable<OrderItemDto> GetUserOrderItemList(int orderId);
	}
}
