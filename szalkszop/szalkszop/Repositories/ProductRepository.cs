using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using System.Data.Entity;


namespace szalkszop.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		// czemu nie lista? - "nie ma sensu aby zeby klient tej klasy dodawal obiekt do listy obiektow z tej metody" ??


		public IEnumerable<Product> GetThreeNewestProducts()
		{
			return _context.Products.OrderByDescending(d => d.DateOfAdding).Take(3).ToList();
		}

		public IEnumerable<Product>GetProductsWithCategory()
		{
			return _context.Products.Include(p => p.ProductCategory).ToList();
		}

		public Product GetEditingProduct(int id)
		{
			return _context.Products.Single(u => u.Id == id);
		}


		public void Add(Product product)
		{
			_context.Products.Add(product);
		}
	}
}