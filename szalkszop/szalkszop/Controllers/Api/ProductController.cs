using System.Web.Http;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers.Api
{
	public class ProductController : ApiController
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost]
		public IHttpActionResult Search(ProductFiltersViewModel dto)
		{
			var productList = _productService.GetQueriedProductListApi(dto);

			return Ok(productList);
		}
	}
}
