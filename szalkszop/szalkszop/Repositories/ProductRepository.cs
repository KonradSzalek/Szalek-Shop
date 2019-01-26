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

		public IQueryable<Product> GetList()
		{
			return _context.Products.Where(p => p.IsActive == true);
		}

		public Product Get(int id)
		{
			return _context.Products.Include(p => p.ProductCategory)
				.Include(p => p.Images)
				.SingleOrDefault(u => u.Id == id);
		}

		public int? GetStockAmount(int id)
		{
			return _context.Products.SingleOrDefault(p => p.Id == id).AmountInStock;
		}

		public void Add(Product product)
		{
			_context.Products.Add(product);
		}

		public void Delete(int id)
		{
			_context.Products.SingleOrDefault(p => p.Id == id).IsActive = false;
			SaveChanges();
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
			bool exists = _context.ProductImages.Any(i => i.Id == id);
			return exists;
		}

		public bool Exists(int id)
		{
			return _context.Products.Any(p => p.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public int GetProductCount()
		{
			return _context.Products.Count(p => p.IsActive == true);
		}

		public List<ProductSearchResultDto> SearchResultFromSqlStoredProcedure(string name, int? priceFrom, int? priceTo, DateTime? dateTimeTo, DateTime? dateTimeFrom, int productCategoryId)
		{
			int? categoryId;
			if (productCategoryId == 0)
			{
				categoryId = null;
			}
			else categoryId = productCategoryId;

			var products = _context.Database.SqlQuery<ProductSearchResultDto>("EXEC [dbo].[SearchProducts] @Name, @PriceFrom, @PriceTo, @DateTimeFrom, @DateTimeTo, @ProductCategoryId",
			new SqlParameter("Name", name ?? SqlString.Null),
			new SqlParameter("PriceTo", priceTo ?? SqlInt32.Null),
			new SqlParameter("PriceFrom", priceFrom ?? SqlInt32.Null),
			new SqlParameter("DateTimeFrom", dateTimeFrom ?? SqlDateTime.Null),
			new SqlParameter("DateTimeTo", dateTimeTo ?? SqlDateTime.Null),
			new SqlParameter("ProductCategoryId", categoryId ?? SqlInt32.Null)).ToList();

			return products;
		}
	}
}