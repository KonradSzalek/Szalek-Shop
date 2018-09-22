using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public class ProductCategoryRepository : IProductCategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductCategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        // cr3 po co ProductCategory jest w nazwie? przeciez to jest productCategory repository wiec chyba jasne
		public IEnumerable<ProductCategory> GetProductCategories()
		{
			return _context.ProductsCategories;
		}

        // cr3 to samo, czemu nie samo "Get"
		public ProductCategory GetProductCategory(int id)
		{
			return _context.ProductsCategories.Single(u => u.Id == id);
		}

		public void Add(ProductCategory category)
		{
			_context.ProductsCategories.Add((category));
		}

		public void DeleteProductCategory(int id)
		{	
			_context.ProductsCategories.Remove(_context.ProductsCategories.Single(p => p.Id == id));
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

        // cr3 super ze umiesciles te metode w repozytorium, nazwa jednak jest tragiczna
        // kazda nazwa jaka zapiszesz musi byc zapisana gramatycznie, mimo ze moze ci sie wydawac inaczej
        // wystarczy samo Exists()
		public bool DoesProductCategoryExist(int id)
		{
			return _context.ProductsCategories.Any(p => p.Id == id);
		}
	}
}