using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductService
	{
		ProductsViewModel GetThreeNewestProductsViewModel();
		ProductsViewModel GetProductsByCategoryViewModel(int categoryId);
		ProductSearchViewModel GetProductSearchViewModel();
		ProductsViewModel GetQueriedProductSearchViewModel(ProductSearchViewModel searchModel);
		ProductsViewModel GetProductsViewModel();
		ProductViewModel AddProductViewModel();
		ProductViewModel EditProductViewModel(int id);
		void AddProduct(ProductViewModel viewModel);
		void EditProduct(ProductViewModel viewModel);
		void DeleteProduct(int id);
		bool ProductExist(int id);
	}
}