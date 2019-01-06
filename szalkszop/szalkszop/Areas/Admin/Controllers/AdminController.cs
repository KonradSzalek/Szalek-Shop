using System.Web.Mvc;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Services;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;
		private readonly IProductService _productService;
		private readonly IUserService _userService;
		private readonly IOrderService _orderService;

		public AdminController(IProductCategoryService productCategoryService, IProductService productService, IUserService userService, IOrderService orderService)
		{
			_productService = productService;
			_productCategoryService = productCategoryService;
			_userService = userService;
			_orderService = orderService;
		}

		public ActionResult Index()
		{
			var viewModel = new AdminViewModel
			{
				ProductCount = _productService.GetProductCount(),
				ProductCategoryCount = _productCategoryService.GetProductCategoryCount(),
				UserCount = _userService.GetUserCount(),
				RecentlyUserCount = _userService.GetRecentlyUserCount(),
				PendingOrderCount = _orderService.GetPendingOrderCount(),
				OrderCount = _orderService.GetOrderCount(),
			};

			return View(viewModel);
		}

		public ActionResult LeftPanel()
		{
			var viewModel = _productCategoryService.GetPopulatedOnlyProductCategoryList();

			return View("~/Areas/Admin/Views/Shared/_LeftPanelAdmin.cshtml", viewModel);
		}
	}
}
