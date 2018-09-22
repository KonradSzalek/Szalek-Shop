using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;

namespace szalkszop.Repositories
{
    // cr3 zastosowales bardzo dobra praktyke tutaj - nie injectujesz do mappera ani repozytorium ani serwisu tylko przyjmujesz w parametrach
    // gotowe do zmapowania dane dzieki temu mapper ten moze byc statyczna klasa
    // zmodyfikuj mapper zeby byl statyczna klasa ze statycznymi metodami
    // tym samym nie bedziesz musial go rejestrowac w DI i mozesz wywalic interfejs
	public class ProductCategoryMapper : IProductCategoryMapper
	{
		public IEnumerable<ProductCategoryDto> MapToDto(IEnumerable<ProductCategory> categories)
		{
			return categories.Select(category => MapToDto(category));
		}

		public ProductCategoryDto MapToDto(ProductCategory productCategory)
		{
			var productCategoryDto = new ProductCategoryDto
			{
				Id = productCategory.Id,
				Name = productCategory.Name,
			};

			return productCategoryDto;
		}

		public IEnumerable<ProductCategoryWithProductCountDto> MapToDtoWithAmountOfProducts(IEnumerable<Product> products, IEnumerable<ProductCategory> categories)
		{
			var categoriesDto = categories.Select(category => new ProductCategoryWithProductCountDto()
			{
				Id = category.Id,
				Name = category.Name,
				AmountOfProducts = products.Count(p => p.ProductCategoryId == category.Id)
			});

			return categoriesDto;
		}
	}
}