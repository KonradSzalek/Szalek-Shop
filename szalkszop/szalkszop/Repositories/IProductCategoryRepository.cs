using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IProductCategoryRepository
	{
		IEnumerable<ProductCategory> GetProductCategoryList();
		ProductCategory GetProductCategory(int id);
		void Add(ProductCategory category);
		void Remove(ProductCategory category);
		void Complete();
	}
}