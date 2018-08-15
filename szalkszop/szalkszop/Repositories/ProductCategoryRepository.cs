using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public class ProductCategoryRepository : IProductCategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductCategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ProductCategory> GetProductCategories()
		{
			return _context.ProductsCategories.ToList();
		}

		public ProductCategory GetEditingProductCategory(int id)
		{
			return _context.ProductsCategories.Single(u => u.Id == id);
		}

		public void Add(ProductCategory category)
		{
			_context.ProductsCategories.Add((category));
		}

		public void Remove(ProductCategory category)
		{
			_context.ProductsCategories.Remove(category);
		}
	}
}