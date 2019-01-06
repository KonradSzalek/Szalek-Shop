using System.Web.Mvc;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Services;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public ActionResult Index()
		{
			var viewModel = new OrderViewModel
			{
				Orders = _orderService.GetOrderList(),
			};

			return View(viewModel);
		}

		public ActionResult Details(int id)
		{
			if (!_orderService.DoesOrderExist(id))
				return HttpNotFound();

			var viewModel = new OrderDetailsViewModel
			{
				OrderItems = _orderService.GetOrderItemList(id),
				Order = _orderService.GetOrder(id),
			};

			return View(viewModel);
		}

		public ActionResult Update(OrderViewModel viewModel)
		{
			_orderService.UpdateStatus(viewModel.OrderId, viewModel.Status);

			return RedirectToAction("Index");
		}
	}
}