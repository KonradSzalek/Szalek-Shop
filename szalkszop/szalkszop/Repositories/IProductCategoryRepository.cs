using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IProductCategoryRepository
	{
		IEnumerable<ProductCategory> GetProductCategories();
		ProductCategory GetProductCategory(int id);
		void Add(ProductCategory category);
		void DeleteProductCategory(int id);
		void SaveChanges();
		bool DoesProductCategoryExist(int id);
	}
}