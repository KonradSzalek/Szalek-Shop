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

		public IEnumerable<ProductCategoryDto> GetProductCategories()
		{
			var categories = _context.ProductsCategories.ToList();

			return categories.Select(n => new ProductCategoryDto()
			{
				Id = n.Id,
				Name = n.Name,
				AmountOfProducts = n.AmountOfProducts,
			});
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

		public IEnumerable<ProductCategoryDto> GetCategoriesWithAmountOfProducts(List<ProductDto> products)
		{
			var categories = GetProductCategories().ToList();

			foreach (var category in categories)
			{
				category.AmountOfProducts = products.Count(p => p.ProductCategory.Id == category.Id);
			}
			return categories;
		}
	}
}