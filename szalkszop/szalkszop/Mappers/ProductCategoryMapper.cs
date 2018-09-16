using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
    // cr2 dodaj interfejs i injectuj ten mapper przez interfejs
	public class ProductCategoryMapper
	{
        // cr2 nie podoba mi sie fakt ze uzywasz tego samego DTO do przypadku w ktorym zwracasz kategorie bez amountofproduct i z amount of products
        // To sa dla mnie 2 rozne dtosy, jest ProductCategorySearchResultDto i ProductCategoryDto, jedno ma amoutOfProducts (ProductCategoryDto) a drugie nie
        // zawsze jak masz sytuacje ze w jednym przypadku uzywasz wszystkich pól a w drugim nie, to oznacza to ze powinienes to rozbic na 2 klasy
		public IEnumerable<ProductCategoryDto> MapToDto(List<ProductCategory> categories)
		{
            // cr2 
            // jak juz robisz 2 rozne mappingi, zbiorowy i pojedynczy to czemu w zbiorowym nie wykorzystasz pojedynczezgo?
            // return categories.Select(category => MapToDto(category))
			return categories.Select(category => new ProductCategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
			});
		}

		public ProductCategoryDto MapToDto(ProductCategory productCategory)
		{
			return new ProductCategoryDto
			{
				Id = productCategory.Id,
				Name = productCategory.Name,
			};
		}

		public IEnumerable<ProductCategoryDto> MapToDtoWithAmountOfProducts(List<ProductDto> products, IEnumerable<ProductCategory> categories)
		{
			var categoriesDto = categories.Select(category => new ProductCategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				AmountOfProducts = products.Count(p => p.ProductCategory.Id == category.Id)
			});

			return categoriesDto;
		}
	}
}