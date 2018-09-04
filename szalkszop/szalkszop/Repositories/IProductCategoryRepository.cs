using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IProductCategoryRepository
	{
		IEnumerable<ProductCategoryDto> GetProductCategories();
		ProductCategory GetEditingProductCategory(int id);
		IEnumerable<ProductCategoryDto> GetCategoriesWithAmountOfProducts(List<ProductDto> products);
		void Add(ProductCategory category);
		void Remove(ProductCategory category);
	}
}