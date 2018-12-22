using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IOrderRepository
	{
		IEnumerable<PaymentMethod> GetPaymentMethodList();
		IEnumerable<DeliveryType> GetDeliveryTypeList();
		void Add(Order order);
		void AddOrderItem(OrderItem orderItem);
		void Delete(int id);
		void SaveChanges();
	}
}
