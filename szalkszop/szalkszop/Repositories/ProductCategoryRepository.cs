using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using System.Data.Entity;

namespace szalkszop.Repositories
{
	public class ProductCategoryRepository : IProductCategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductCategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ProductCategory> GetList()
		{
			return _context.ProductsCategories.Where(pc => pc.IsActive == true);
		}

		public ProductCategory Get(int id)
		{
			return _context.ProductsCategories.Single(u => u.Id == id);
		}

		public void Add(ProductCategory category)
		{
			_context.ProductsCategories.Add((category));
		}

		public void Delete(int id)
		{
			var products = _context.Products.Include(p => p.ProductCategory).Include(i => i.Images).Where(x => x.ProductCategoryId == id);

			foreach (var product in products.ToList())
			{
				_context.Products.SingleOrDefault(p => p.Id == product.Id).IsActive = false;
			}

			_context.ProductsCategories.SingleOrDefault(pc => pc.Id == id).IsActive = false;
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public bool Exists(int id)
		{
			return _context.ProductsCategories.Any(p => p.Id == id);
		}

		public int GetProductCategoryCount()
		{
			return _context.ProductsCategories.Count(pc => pc.IsActive == true);
		}
	}
}