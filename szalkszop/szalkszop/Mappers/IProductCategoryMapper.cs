using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Mappers
{
	public interface IProductCategoryMapper
	{
		IEnumerable<ProductCategoryDto> MapToDto(IEnumerable<ProductCategory> categories);
		ProductCategoryDto MapToDto(ProductCategory productCategory);
		IEnumerable<ProductCategoryWithProductCountDto> MapToDtoWithAmountOfProducts(IEnumerable<Product> products, IEnumerable<ProductCategory> categories);
	}
}