using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
    // cr2 stworz interfejs pod te klase i injectuj go w kontrolerze przy pomocy interfejsu 
	public class ProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly ProductMapper _productMapper;

		public ProductService(IProductRepository productRepository, ProductMapper productMapper)
		{
			_productRepository = productRepository;
			_productMapper = productMapper;
		}

		public ProductDto GetEditingProductDto(int id)
		{
			var product = _productRepository.GetProduct(id);

			return _productMapper.MapToDto(product);
		}

        // cr2 nie eksponuj Modelu do kontrolera, service ma zwracac jedynie DTO
		public Product GetEditingProduct(int id)
		{
			return  _productRepository.GetProduct(id);	
		}

		public IEnumerable<ProductDto> GetThreeNewestProducts()
		{
            // cr2 nie uwazasz ze bardziej czytelne bedzie jak kazda metode na osobna linijke jebniesz? np.:
            //_productRepository.GetProductList()
            //    .Include(p => p.ProductCategory)
            //    .OrderByDescending(d => d.DateOfAdding)
            //    .Take(3)
            //    .ToList();

			var products = _productRepository.GetProductList().Include(p => p.ProductCategory).OrderByDescending(d => d.DateOfAdding).Take(3).ToList();

			return _productMapper.MapToDto(products);
		}

		public IEnumerable<ProductDto> GetProductsWithCategory()
		{
			var products = _productRepository.GetProductList().Include(p => p.ProductCategory).ToList();

			return _productMapper.MapToDto(products);
		}

		public IEnumerable<ProductDto> GetProductInCategory(int id)
		{
            // cr2 to samo co wyzej ja bym dal metode linq per linijka w takim skomplikowanym przypadku
			var products = _productRepository.GetProductList().Include(p => p.ProductCategory).Where(p => p.ProductCategoryId == id).ToList();

			return _productMapper.MapToDto(products);
		}

		public IEnumerable<ProductDto> GetQueriedProducts(ProductSearchModel searchModel, IEnumerable<ProductDto> products)
		{
			if (searchModel != null)
			{
				if (searchModel.Id.HasValue)
					products = products.Where(p => p.Id == searchModel.Id);
				if (!string.IsNullOrEmpty(searchModel.Name))
					products = products.Where(p => p.Name.Contains(searchModel.Name));
				if (searchModel.PriceFrom.HasValue)
					products = products.Where(p => p.Price >= searchModel.PriceFrom);
				if (searchModel.PriceTo.HasValue)
					products = products.Where(p => p.Price <= searchModel.PriceTo);
				if (searchModel.DateTimeFrom.HasValue)
					products = products.Where(p => p.DateOfAdding >= searchModel.DateTimeFrom);
				if (searchModel.DateTimeTo.HasValue)
					products = products.Where(p => p.DateOfAdding <= searchModel.DateTimeTo);
				if (searchModel.ProductCategory.Id != 0)
					products = products.Where(p => p.ProductCategory.Id == searchModel.ProductCategory.Id);
			}
			return products;
		}

		public void Add(Product product)
		{
			_productRepository.Add(product);
            // cr2 uzyj complete
		}

		public void Remove(Product product)
		{
			_productRepository.Remove(product);
            // cr2 uzyj complete
		}

        // cr2 nie eksponuj metody complete na zewnatrz
		public void Complete()
		{
			_productRepository.Complete();
		}

	}
}