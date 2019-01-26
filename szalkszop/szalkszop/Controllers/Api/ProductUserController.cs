using System.Web.Http;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers.Api
{
	public class ProductUserController : ApiController
	{
		private readonly IProductService _productService;

		public ProductUserController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost]
		public IHttpActionResult Search(ProductFiltersViewModel dto)
		{
			var productList = _productService.GetQueriedProductListApiUser(dto);

			return Ok(productList);
		}
	}
}
