﻿using System.Collections.Generic;
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

		public string IsUserAuthorized(int orderId)
		{
			return _context.Orders.SingleOrDefault(o => o.Id == orderId).CustomerId;
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

		public void Cancel(int id)
		{
			_context.Orders.SingleOrDefault(o => o.Id == id).Status = Order.OrderStatus.Canceled;
		}

		public Order GetOrder(int id)
		{
			return _context.Orders.Include(pm => pm.PaymentMethod).Include(dt => dt.DeliveryType).Single(o => o.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public int GetPendingOrderCount()
		{
			return _context.Orders.Where(o => o.Status == 0).Count();
		}

		public int GetOrderCount()
		{
			return _context.Orders.Count();
		}

		public bool Exists(int id)
		{
			return _context.Orders.Any(o => o.Id == id);
		}
	}
}