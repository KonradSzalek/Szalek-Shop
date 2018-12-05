using System;
using System.Collections.Generic;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductService
	{
		IEnumerable<ProductDto> GetThreeNewestProducts();
		ProductsViewModel GetProductsByCategoryViewModel(int categoryId);
		ProductSearchViewModel GetProductSearchViewModel();
		IEnumerable<ProductSearchResult> GetQueriedProducts(ProductSearchViewModel searchModel);
		IEnumerable<ProductDto> GetProducts();
		ProductViewModel AddProductViewModel();
		ProductViewModel EditProductViewModel(int id);
		ProductDetailViewModel ProductDetailViewModel(int id);
		void AddProduct(ProductViewModel viewModel);
		void EditProduct(ProductViewModel viewModel);
		void DeleteProduct(int id);
		bool ProductExist(int id);
		bool ProductPhotoExists(Guid id);
		void DeletePhoto(Guid id);
	}
}