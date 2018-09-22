using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IProductService
	{

		// Home Controller
		ProductViewModel GetThreeNewestProductsViewModel();
		ProductViewModel GetShowProductInCategoryViewModel(int id);

		// Home Controller & Product Controller
		ProductSearchModel GetProductSearchViewModel(); 
		ProductViewModel GetQueriedProductSearchViewModel(ProductSearchModel searchModel); 
		ProductViewModel GetProductViewModel(); 
		
		// Product Controller
		ProductViewModel AddProductViewModel();
		ProductViewModel EditProductViewModel(int id);
		void AddProduct(ProductViewModel viewModel);
		void EditProduct(ProductViewModel viewModel);
		void DeleteProduct(int id);
		bool IsProductExist(int id);


		//IEnumerable<ProductDto> GetProductsWithCategory();
		//IEnumerable<ProductDto> GetProductInCategory(int id);
		//IEnumerable<ProductDto> GetQueriedProducts(ProductSearchModel searchModel, IEnumerable<ProductDto> products);
		//ProductDto GetEditingProductDto(int id);
		//Product GetEditingProduct(int id);	
	}
}