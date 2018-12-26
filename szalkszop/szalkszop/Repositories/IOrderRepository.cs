using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.ViewModels;

namespace szalkszop.Repositories
{
	public interface IOrderRepository
	{
		IEnumerable<PaymentMethod> GetPaymentMethodList();
		IEnumerable<DeliveryType> GetDeliveryTypeList();
		IEnumerable<Order> GetOrderList(string userId);
		IEnumerable<OrderItem> GetOrderItemList(int id);
		void Add(Order order);
		void AddOrderItem(OrderItem orderItem);
		void Delete(int id);
		void SaveChanges();
	}
}
