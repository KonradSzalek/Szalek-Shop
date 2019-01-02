using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IPaymentMethodRepository
	{
		void Add(PaymentMethod paymentMethod);
		void Remove(int id);
		bool Exists(int id);
		void SaveChanges();
		IEnumerable<PaymentMethod> GetPaymentMethodList();
		PaymentMethod Get(int id);
	}
}
