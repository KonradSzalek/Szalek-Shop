using System.Collections.Generic;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductCategoryService
	{
		IEnumerable<ProductCategoryDto> GetProductCategoryList();
		AdminProductCategoryListViewModel GetProductCategoryWithProductCountList();
		UserProductCategoryListViewModel GetPopulatedOnlyProductCategoryList();
		AdminProductCategoryViewModel EditProductCategory(int id);
		void AddProductCategory(AdminProductCategoryViewModel viewModel);
		void EditProductCategory(AdminProductCategoryViewModel viewModel);
		void DeleteProductCategory(int id);
		bool DoesProductCategoryExist(int id);
	}
}