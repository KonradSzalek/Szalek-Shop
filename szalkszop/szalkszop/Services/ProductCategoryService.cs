using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;

namespace szalkszop.Services
{
    // cr2 stworz interfejs pod te klase i injectuj go w kontrolerze przy pomocy interfejsu 
	public class ProductCategoryService
	{
        // cr2 injectuj przez interfejsy a nie konkretne klasy
		private readonly ProductCategoryRepository _productCategoryRepository;
		private readonly ProductCategoryMapper _productCategoryMapper;

		public ProductCategoryService(ProductCategoryRepository productCategoryRepository, ProductCategoryMapper productCategoryMapper)
		{
			_productCategoryRepository = productCategoryRepository;
			_productCategoryMapper = productCategoryMapper;
		}

		public IEnumerable<ProductCategoryDto> GetProductCategories()
		{
			var categories = _productCategoryRepository.GetProductCategoryList().ToList();

			return _productCategoryMapper.MapToDto(categories);
		}

		public ProductCategoryDto GetEditingProductCategoryDto(int id)
		{
			var productCategory = _productCategoryRepository.GetProductCategory(id);

			return _productCategoryMapper.MapToDto(productCategory);
		}

        // cr2 nie eksponuj na kontrolerowi modelu, jedynie DTOsy, wywal te metode
		public ProductCategory GetEditingProductCategory(int id)
		{
			return _productCategoryRepository.GetProductCategory(id);
		}

        // cr2 nie pobieraj produktow jako parametr tylko uzyj productRepository, na pobranych produktach nie uzywaj ToList tylko przekaz jako ienumerable
		public IEnumerable<ProductCategoryDto> GetCategoriesWithAmountOfProducts(List<ProductDto> products)
		{
			var categories = _productCategoryRepository.GetProductCategoryList();

            // cr2 to sie da zrobic jeszcze lepiej bez udzialu mappera
            // trzeba uzyc linq query syntax i zrobic joina jak na bazie danych
            // obawiam sie ze bez wczesniejszej zabawy z baza danych tego nie ogarniesz wiec poki co zobacz sobie w googlach
            // Linq query syntax
			return _productCategoryMapper.MapToDtoWithAmountOfProducts(products, categories);
		}

		public void Add(ProductCategory category)
		{
			_productCategoryRepository.Add((category));

            // cr2 dodaj uzycie Complete
		}

		public void Remove(ProductCategory category)
		{
            // cr2 nie musisz pobierac kategorii zeby ja usunac
            // zmien te metode zeby przyjmowala Id i usuwala po Id
            // repozytorium niech tez przyjmie id zamiast obiektu konkretnego
			_productCategoryRepository.Remove(category);

            // cr2 dodaj uzycie Complete
		}

        // cr2 wywal te metode stad, niech serwis sam wywoluje ja na repozytorium
		public void Complete()
		{
			_productCategoryRepository.Complete();
		}
	}
}