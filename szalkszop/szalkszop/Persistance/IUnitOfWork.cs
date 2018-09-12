using szalkszop.Repositories;

namespace szalkszop.Persistance
{
    // cr1: do wywalenia
	public interface IUnitOfWork
	{
		IProductRepository Products { get; }
		IProductCategoryRepository ProductCategories { get; }
		IUserRepository UserRepository { get; }

		void Complete();
	}
}