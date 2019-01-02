using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IDeliveryTypeRepository
	{
		void Add(DeliveryType deliveryType);
		void Remove(int id);
		bool Exists(int id);
		void SaveChanges();
		IEnumerable<DeliveryType> GetList();
		DeliveryType Get(int Id);
	}
}
