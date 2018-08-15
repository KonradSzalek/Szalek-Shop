using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IProductCategoryRepository
	{
		IEnumerable<ProductCategory> GetProductCategories();
		ProductCategory GetEditingProductCategory(int id);
		void Add(ProductCategory category);
		void Remove(ProductCategory category);
	}
}