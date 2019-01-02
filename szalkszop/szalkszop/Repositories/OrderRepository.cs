using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;

		public OrderRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Add(Order order)
		{
			_context.Orders.Add(order);
		}

		public void AddOrderItem(OrderItem orderItem)
		{
			_context.OrdersItems.Add(orderItem);
		}

		public IEnumerable<Order> GetUserOrderList(string userId)
		{
			return _context.Orders.Include(pm => pm.PaymentMethod).Include(dt => dt.DeliveryType).Where(o => o.CustomerId == userId);
		}

		public IEnumerable<Order> GetList()
		{
			return _context.Orders.Include(pm => pm.PaymentMethod).Include(dt => dt.DeliveryType);
		}

		public IEnumerable<OrderItem> GetOrderItemList(int id)
		{
			return _context.OrdersItems.Where(oi => oi.OrderId == id);
		}

		public void Delete(int id)
		{
			_context.Orders.Remove(_context.Orders.Single(p => p.Id == id));
		}

		public Order GetOrder(int id)
		{
			return _context.Orders.Single(o => o.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}