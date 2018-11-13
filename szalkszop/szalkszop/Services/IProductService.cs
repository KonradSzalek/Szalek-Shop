using System;
using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductService
	{
		IEnumerable<ProductDto> GetThreeNewestProducts();
		ProductsViewModel GetProductsByCategoryViewModel(int categoryId);
		ProductSearchViewModel GetProductSearchViewModel();
		IEnumerable<ProductDto> GetQueriedProductSearch(ProductSearchViewModel searchModel);
		IEnumerable<ProductDto> GetProducts();
		ProductViewModel AddProductViewModel();
		ProductViewModel EditProductViewModel(int id);
		int AddProduct(ProductViewModel viewModel);
		void EditProduct(ProductViewModel viewModel);
		void DeletePhoto(Guid id, int productId);
		void DeleteProduct(int id);
		bool ProductExist(int id);
	}
}