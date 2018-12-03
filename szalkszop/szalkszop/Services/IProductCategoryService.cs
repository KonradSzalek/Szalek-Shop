using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductCategoryService
	{
		IEnumerable<ProductCategoryDto> GetProductCategoriesList();
		AdminProductCategoriesViewModel GetAdminProductCategoriesViewModel();
		UserProductCategoriesViewModel GetUserProductCategoriesViewModel(); 
		AdminProductCategoryViewModel EditProductCategoryViewModel(int id);
		void AddProductCategory(AdminProductCategoryViewModel viewModel);
		void EditProductCategory(AdminProductCategoryViewModel viewModel);
		void DeleteProductCategory(int id);
		bool ProductCategoryExist(int id);
	}
}