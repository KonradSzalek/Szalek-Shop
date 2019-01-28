using System.Web.Http;
using szalkszop.Services;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Controllers.Api
{
	public class ProductController : ApiController
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[AuthorizeRedirectToHomePage(Roles = "Admin")]
		[HttpPost]
		public IHttpActionResult AdminSearch(ProductFiltersViewModel dto)
		{
			var productList = _productService.GetAdminQueriedProductList(dto);

			return Ok(productList);
		}

		[Authorize]
		[HttpPost]
		public IHttpActionResult UserSearch(ProductFiltersViewModel dto)
		{
			var productList = _productService.GetUserQueriedProductList(dto);

			return Ok(productList);
		}
	}
}
