using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.ViewModels;

namespace szalkszop.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public DbSet<Product> GetList()
		{
			return _context.Products;
		}

		public Product Get(int id)
		{
			return _context.Products.Include(p => p.ProductCategory)
				.Include(p => p.Images)
				.Single(u => u.Id == id);
		}

		public void Add(Product product)
		{
			_context.Products.Add(product);
		}

		public void Delete(int id)
		{
			var product = _context.Products.Include(i => i.Images).Single(p => p.Id == id);

			foreach (var image in product.Images.ToList())
			{
				var im = _context.ProductImages.Single(i => i.Id == image.Id);
				_context.ProductImages.Remove(im);
			}
	
			_context.Products.Remove(_context.Products.Single(p => p.Id == id));
		}

		public void DeletePhoto(Guid id)
		{
			_context.ProductImages.Remove(_context.ProductImages.FirstOrDefault(i => i.Id == id));	
		}

		public List<string> GetPhotosNames(Guid id)
		{
			List<string> imageNames = new List<string>();

			imageNames.Add(_context.ProductImages.FirstOrDefault(i => i.Id == id).ImageName);
			imageNames.Add(_context.ProductImages.FirstOrDefault(i => i.Id == id).ThumbnailName);

			return imageNames;
		}

		public bool PhotoExists(Guid id)
		{
			return _context.ProductImages.Any(i => i.Id == id);
		}

		public bool Exists(int id)
		{
			return _context.Products.Any(p => p.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public List<ProductSearchResult> SearchResultFromSqlStoredProcedure(string name, int? priceFrom, int? priceTo, DateTime? dateTimeFrom, DateTime? dateTimeTo, int productCategoryId)
		{
			int? categoryId;
			if (productCategoryId == 0)
			{
				categoryId = null;
			}
			else categoryId = productCategoryId;

			var products = _context.Database.SqlQuery<ProductSearchResult>("EXEC [dbo].[SearchProductsStoredProcedure] @Name, @PriceFrom, @PriceTo, @DateTimeFrom, @DateTimeTo, @ProductCategoryId",
			new SqlParameter("Name", name ?? SqlString.Null),
			new SqlParameter("PriceFrom", priceFrom ?? SqlInt32.Null),
			new SqlParameter("PriceTo", priceTo ?? SqlInt32.Null),
			new SqlParameter("DateTimeFrom", dateTimeFrom ?? SqlDateTime.Null),
			new SqlParameter("DateTimeTo", dateTimeTo ?? SqlDateTime.Null),
			new SqlParameter("ProductCategoryId", categoryId ?? SqlInt32.Null)).ToList();

			return products;
		}
	}
}