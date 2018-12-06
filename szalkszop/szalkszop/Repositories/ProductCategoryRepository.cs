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
			return _context.ProductsCategories;
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
				foreach (var image in product.Images.ToList())
				{
					var im = _context.ProductImages.Single(i => i.Id == image.Id);
					_context.ProductImages.Remove(im);
				}
				_context.Products.Remove(product);
			}
	
			_context.ProductsCategories.Remove(_context.ProductsCategories.Single(p => p.Id == id));
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public bool Exists(int id)
		{
			return _context.ProductsCategories.Any(p => p.Id == id);
		}
	}
}