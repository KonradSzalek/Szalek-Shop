using szalkszop.Core.Models;
using szalkszop.Repositories;

namespace szalkszop.Persistance
{
    // cr1: Wywal unit of work z projektu, do kontrolerow injectuj jedynie repozytoria ktorych tam uzywasz. W metodach repozytoriow ktore modyfikuja rekordy wywoluj "savechanges"
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