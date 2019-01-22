using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetUserOrderList(string userId);
		IEnumerable<OrderItem> GetOrderItemList(int id);
		IEnumerable<Order> GetList();
		string IsUserAuthorized(int orderId);
		void Add(Order order);
		void AddOrderItem(OrderItem orderItem);
		void Cancel(int id);
		void Delete(int id);
		void SaveChanges();
		Order GetOrder(int Id);
		int GetPendingOrderCount();
		int GetOrderCount();
		bool Exists(int id);
	}
}
