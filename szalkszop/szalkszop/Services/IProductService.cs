using System;
using System.Collections.Generic;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductService
	{
		IEnumerable<ProductDto> GetThreeNewestProducts();
		ProductListViewModel GetProductListByCategory(int categoryId);
		ProductFiltersViewModel GetProductSearch();
		IEnumerable<ProductSearchResultDto> GetQueriedProductList(ProductFiltersViewModel searchModel);
		IEnumerable<ProductDto> GetProductList();
		ProductViewModel AddProduct();
		ProductViewModel EditProduct(int id);
		ProductDetailViewModel GetProductDetail(int id);
		ProductDto GetProduct(int id);
		List<ProductImageDto> GetProductImages(int id);
		void AddProduct(ProductViewModel viewModel);
		void EditProduct(ProductViewModel viewModel);
		void DeleteProduct(int id);
		bool DoesProductExist(int id);
		bool DoesProductPhotoExist(Guid id);
		bool IsPhotoCountExceeded(int productId, int filesCount);
		void DeletePhoto(Guid id);
		int GetProductCount();
	}
}