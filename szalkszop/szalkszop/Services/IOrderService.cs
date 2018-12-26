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
		IEnumerable<PaymentMethodDto> GetPaymentMethodList();
		IEnumerable<DeliveryTypeDto> GetDeliveryTypeList();
		void CompleteOrder(OrderViewModel viewModel, string userId);
		OrderListViewModel GetUserOrderList(string userId);
	}
}
