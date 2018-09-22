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
		ProductCategoriesWithProductCountViewModel GetProductCategoriesWithProductCountViewModel();
		ProductCategoriesViewModel GetProductCategoriesViewModel();
		ProductCategoryViewModel EditProductCategoryViewModel(int id);
		void AddProductCategory(ProductCategoryViewModel viewModel);
		void EditProductCategory(ProductCategoryViewModel viewModel);
		void DeleteProductCategory(int id);
		bool ProductCategoryExist(int id);
	}
}