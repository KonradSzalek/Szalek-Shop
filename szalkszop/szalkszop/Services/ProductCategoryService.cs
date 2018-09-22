using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductRepository _productRepository;
		private readonly IProductCategoryMapper _productCategoryMapper;

		// cr3 musze napisac pare zdan o odpowiedzialnosci serwisu
		// serwis powinien komunikowac sie z repozytorium zeby zwracac kontrolerowi dane oraz zeby przyjmowac dane od kontrolera i wrzucac je do bazy
		// serwis nie powinien wiedziec o tym ze ma ustawic jakis heading w viewmodelu, bo sam w sobie o widokach serwis nic nie wie
		// tak samo serwis nie powinien przygotowywac pustego viewmodelu kontrolerowi do przekazania na widok, to powinien robic kontroler
		// tak na marginesie to ta metoda powinna byc prywatna bo zwraca model a nie chcesz nigdzie wyzej tego udostepniac 

		public ProductCategoryService(ProductCategoryRepository productCategoryRepository, ProductCategoryMapper productCategoryMapper, ProductRepository productRepository)
		{
			_productCategoryRepository = productCategoryRepository;
			_productCategoryMapper = productCategoryMapper;
			_productRepository = productRepository;
		}

		public IEnumerable<ProductCategoryDto> GetProductCategoriesList()
		{
			var productCategories = _productCategoryRepository.GetProductCategories();

			return _productCategoryMapper.MapToDto(productCategories);
		}

		public IEnumerable<ProductCategoryWithProductCountDto> GetCategoriesWithAmountOfProducts()
		{
			var products = _productRepository.GetProductList();
			var categories = _productCategoryRepository.GetProductCategories();

			return _productCategoryMapper.MapToDtoWithAmountOfProducts(products, categories);
		}

		public void AddProductCategory(ProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_productCategoryRepository.Add(category);
			_productCategoryRepository.SaveChanges();
		}

		public void EditProductCategory(ProductCategoryViewModel viewModel)
		{
			var category = _productCategoryRepository.GetProductCategory(viewModel.Id);

			category.Name = viewModel.Name;

			_productCategoryRepository.SaveChanges();
		}

		public void DeleteProductCategory(int id)
		{
			_productCategoryRepository.DeleteProductCategory(id);
			_productCategoryRepository.SaveChanges();
		}

		public bool ProductCategoryExist(int id)
		{
			return _productCategoryRepository.DoesProductCategoryExist(id);
		}


		public ProductCategoriesWithProductCountViewModel GetProductCategoriesWithProductCountViewModel()
		{
			var viewModel = new ProductCategoriesWithProductCountViewModel
			{
				ProductCategoriesWithProductCountDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoriesViewModel GetProductCategoriesViewModel()
		{
			var productCategories = _productCategoryRepository.GetProductCategories();
			var productCategoriesDto = _productCategoryMapper.MapToDto(productCategories);

			var viewModel = new ProductCategoriesViewModel
			{
				ProductCategoriesDto = productCategoriesDto,
			};

			return viewModel;
		}

		public ProductCategoryViewModel EditProductCategoryViewModel(int id)
		{
			var productCategory = _productCategoryRepository.GetProductCategory(id);

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Update Category",
				Name = productCategory.Name,
				Id = productCategory.Id,
			};

			return viewModel;
		}
	}
}