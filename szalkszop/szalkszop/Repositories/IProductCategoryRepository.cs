using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IProductCategoryRepository
	{
		IEnumerable<ProductCategory> GetList();
		ProductCategory Get(int id);
		void Add(ProductCategory category);
		void Delete(int id);
		void SaveChanges();
		bool Exists(int id);
	}
}