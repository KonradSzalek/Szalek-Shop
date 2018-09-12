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

        // cr1: Repozytoria powinny zwracac Model i przyjmowac model. 
        // Repozytorium wie jedynie o modelu, nie powinnien wiedziec o zadnych strukturach ktore zyja poza nim
        // MApping nie powinien sie dziac w tym miejscu, powinienes go robic w kontrolerze lub specjalnymmapperze (mozemy o tym pogadac wiecej na zywo)
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

        // cr1: tak jak wyzej
		public IEnumerable<ProductCategoryDto> GetCategoriesWithAmountOfProducts(List<ProductDto> products)
		{
			var categories = GetProductCategories().ToList();

			foreach (var category in categories)
			{
                // cr1: to nie musi byc odseparowane od mappingu na CategoryDto, linq jest bardzo elastyczne
                // linijki products.Count(p => p.ProductCategory.Id == category.Id) mozesz uzyc w lambda expression przy tworzeniu obiektu
				category.AmountOfProducts = products.Count(p => p.ProductCategory.Id == category.Id);
			}
			return categories;
		}
	}
}