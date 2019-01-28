using System.Collections.Generic;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductCategoryService
	{
		IEnumerable<ProductCategoryDto> GetProductCategoryList();
		IEnumerable<ProductCategoryWithProductCountDto> GetProductCategoryWithProductCountList();
		UserProductCategoryListViewModel GetPopulatedOnlyProductCategoryList();
		ProductCategoryViewModel EditProductCategory(int id);
		void AddProductCategory(ProductCategoryViewModel viewModel);
		void EditProductCategory(ProductCategoryViewModel viewModel);
		void DeleteProductCategory(int id);
		bool DoesProductCategoryExist(int id);
		int GetProductCategoryCount();
	}
}