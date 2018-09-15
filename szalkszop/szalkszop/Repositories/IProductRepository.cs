using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Repositories
{
	public interface IProductRepository
	{
		IEnumerable<ProductDto> GetThreeNewestProducts();
		IEnumerable<ProductDto> GetProductsWithCategory();
		Product GetEditingProduct(int id);
		void Add(Product product);
		void Remove(Product product);
		IEnumerable<ProductDto> GetProductInCategory(int id);
		IEnumerable<ProductDto> GetQueriedProducts(ProductSearchModel searchModel, IEnumerable<ProductDto> products);
		void Complete();
	}
}