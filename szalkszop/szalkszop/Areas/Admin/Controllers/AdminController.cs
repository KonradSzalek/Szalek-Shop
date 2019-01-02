using System.Web.Mvc;
using szalkszop.Services;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	//CR5FIXED ten rpzedrostek niepotrzebnu
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;

		public AdminController(ProductCategoryService productCategoryService, ProductService productService)
		{
			_productCategoryService = productCategoryService;
		}

		//CR5FIXED usun ten pusty controller
		// - jest potrzebny mimo ze widok na razie jest pusty
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult LeftPanel()
		{
			var viewModel = _productCategoryService.GetPopulatedOnlyProductCategoryList();
			return View("_LeftPanelAdmin", viewModel);
		}
	}
}
