using szalkszop.Core.Models;
using szalkszop.Repositories;

namespace szalkszop.Persistance
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public IProductRepository Products { get; private set; }
		public IProductCategoryRepository ProductCategories { get; private set; }
		public IUserRepository UserRepository { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Products = new ProductRepository(context);
			ProductCategories = new ProductCategoryRepository(context);
			UserRepository = new UserRepository(context);
		}

		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}