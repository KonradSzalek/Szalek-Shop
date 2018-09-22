using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Mappers
{
	public interface IProductMapper
	{
		IEnumerable<ProductDto> MapToDto(IEnumerable<Product> products);
		ProductDto MapToDto(Product product);
	}
}