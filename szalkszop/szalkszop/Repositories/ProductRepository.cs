using System.Data.Entity;
using System.Linq;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public DbSet<Product> GetProductList()
		{
			return _context.Products;
		}

		public Product GetProduct(int id)
		{
			return _context.Products.Include(p => p.ProductCategory).Single(u => u.Id == id);
		}

		public void Add(Product product)
		{
			_context.Products.Add(product);
		}

		public void Remove(int id)
		{
			_context.Products.Remove(_context.Products.Single(p => p.Id == id));
		}

		public bool DoesProductExist(int id)
		{
			return _context.Products.Any(p => p.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}