using System.Collections.Generic;
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

		public IEnumerable<PaymentMethod> GetPaymentMethodList()
		{
			return _context.PaymentMethods.AsEnumerable();
		}

		public IEnumerable<DeliveryType> GetDeliveryTypeList()
		{
			return _context.DeliveryTypes.AsEnumerable();
		}

		public void Add(Order order)
		{
			_context.Orders.Add(order);
		}

		public void AddOrderItem(OrderItem orderItem)
		{
			_context.OrdersItems.Add(orderItem);
		}

		public void Delete(int id)
		{
			_context.Orders.Remove(_context.Orders.Single(p => p.Id == id));
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}