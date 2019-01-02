using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public class DeliveryTypeRepository : IDeliveryTypeRepository
	{
		private readonly ApplicationDbContext _context;

		public DeliveryTypeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Add(DeliveryType deliveryType)
		{
			_context.DeliveryTypes.Add(deliveryType);
		}

		public void Remove(int id)
		{
			_context.DeliveryTypes.Remove(_context.DeliveryTypes.Single(dt => dt.Id == id));
		}

		public bool Exists(int id)
		{
			return _context.DeliveryTypes.Any(dt => dt.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IEnumerable<DeliveryType> GetList()
		{
			return _context.DeliveryTypes.AsEnumerable();
		}

		public DeliveryType Get(int Id)
		{
			return _context.DeliveryTypes.Single(dt => dt.Id == Id);
		}
	}
}