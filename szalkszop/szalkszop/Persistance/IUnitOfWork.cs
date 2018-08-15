using szalkszop.Repositories;

namespace szalkszop.Persistance
{
	public interface IUnitOfWork
	{
		IProductRepository Products { get; }
		IProductCategoryRepository ProductCategories { get; }
		IUserRepository UserRepository { get; }

		void Complete();
	}
}