using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetThreeNewestProducts();
		IEnumerable<Product>GetProductsWithCategory();
		Product GetEditingProduct(int id);
		void Add(Product product);
	}
}