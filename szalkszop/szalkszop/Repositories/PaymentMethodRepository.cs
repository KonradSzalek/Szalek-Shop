using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public class PaymentMethodRepository : IPaymentMethodRepository
	{
		private readonly ApplicationDbContext _context;

		public PaymentMethodRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Add(PaymentMethod paymentMethod)
		{
			_context.PaymentMethods.Add(paymentMethod);
		}

		public void Remove(int id)
		{
			_context.PaymentMethods.Remove(_context.PaymentMethods.Single(pm => pm.Id == id));
		}

		public bool Exists(int id)
		{
			return _context.PaymentMethods.Any(dt => dt.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IEnumerable<PaymentMethod> GetPaymentMethodList()
		{
			return _context.PaymentMethods.AsEnumerable();
		}

		public PaymentMethod Get(int id)
		{
			return _context.PaymentMethods.SingleOrDefault(pm => pm.Id == id);
		}
	}
}