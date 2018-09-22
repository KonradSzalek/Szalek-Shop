using System;
using System.Collections.Generic;
using System.Linq;
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

		public ProductCategoryService(ProductCategoryRepository productCategoryRepository, ProductCategoryMapper productCategoryMapper, ProductRepository productRepository)
		{
			_productCategoryRepository = productCategoryRepository;
			_productCategoryMapper = productCategoryMapper;
			_productRepository = productRepository;
		}

        // cr3 ta metoda jest uzywana jedynie w jednej metodzie ProductCategoryService
        // nie widze potrzeby jej istnienia, pomniewaz ani nie zawiera oni skomplikowanego kodu ani nie jest reuzywana
        // wywal ja
        // tak na marginesie to ta metoda powinna byc prywatna bo zwraca model a nie chcesz nigdzie wyzej tego udostepniac
		public IEnumerable<ProductCategory> GetProductCategories()
		{
			var categories = _productCategoryRepository.GetProductCategoryList().ToList();

			return categories;
		}

        // cr3 po pierwsze nie wiem po co editing jest w nazwie po drugie ta metoda jest uzywana tylko raz, w tym samym serwisie, po prostu przenies te jedna linijke tam gdzie trzeba
		public ProductCategoryDto GetEditingProductCategoryDto(int id)
		{
			var productCategory = _productCategoryRepository.GetProductCategory(id);

			return _productCategoryMapper.MapToDto(productCategory);
		}

		public IEnumerable<ProductCategorySearchResultDto> GetCategoriesWithAmountOfProducts()
		{		
			var products = _productRepository.GetProductList();
			var categories = _productCategoryRepository.GetProductCategoryList();
            
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
			_productCategoryRepository.Remove(id);
			_productCategoryRepository.SaveChanges();
		}

        // cr3 gdzies juz napisaelm ze nazwa z dupy dla pewnosci pisze i tutaj
		public bool IsProductCategoryExist(int id)
		{
			return _productCategoryRepository.IsCategoryExist(id);
		}

        // cr3 serwis nie powinien wiedziec ze dany viewmodel bedzie partialem lub nie
		public ProductCategoryViewModel GetPartialCategoryView()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategoriesSearchResultDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoryViewModel GetProductCategorySearchResultViewModel()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories", // serwis nie powinien wiedziec o takich rzeczach jak heading
				ProductCategoriesSearchResultDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

		public ProductCategoryViewModel GetProductCategoryViewModel()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategoriesSearchResultDto = GetCategoriesWithAmountOfProducts(),
			};

			return viewModel;
		}

        // cr3 ten kod powinien byc w kontrolerze
        public ProductCategoryViewModel AddProductCategoryViewModel()
        {
            var viewModel = new ProductCategoryViewModel
            {
                Heading = "Add a new category",
            };

            return viewModel;
        }

        // cr3 Ten kod powinien byc w kontrolerze
        public ProductCategoryViewModel EditProductCategoryViewModel(int id)
        {
            var category = GetEditingProductCategoryDto(id);

            var viewModel = new ProductCategoryViewModel
            {
                Heading = "Update Category",
                Name = category.Name,
            };

            return viewModel;
        }
    }
}