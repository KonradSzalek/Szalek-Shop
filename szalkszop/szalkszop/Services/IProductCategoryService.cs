using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
    // cr3 wywal te komentarze, wystarczy wejsc w referencje zeby widziec kto uzywa danej metody
    // dodatkowo wywal zakomentowaane metody, od tego masz system kontroli wersji zeby nie bac sie wywalania rzeczy
	public interface IProductCategoryService
	{
		// Home Controller

		ProductCategoryViewModel GetPartialCategoryView();
		ProductCategoryViewModel GetProductCategorySearchResultViewModel();

		// Home Controller & Category Controller
		ProductCategoryViewModel GetProductCategoryViewModel(); 

		// Category Controller
		ProductCategoryViewModel AddProductCategoryViewModel();
		ProductCategoryViewModel EditProductCategoryViewModel(int id);
		void AddProductCategory(ProductCategoryViewModel viewModel);
		void EditProductCategory(ProductCategoryViewModel viewModel);
		void DeleteProductCategory(int id);
        // cr3 ProductCategoryExists
		bool IsProductCategoryExist(int id);

		//IEnumerable<ProductCategory> GetProductCategories();
		//IEnumerable<ProductCategorySearchResultDto> GetCategoriesWithAmountOfProducts();
		//ProductCategoryDto GetEditingProductCategoryDto(int id);
	}
}