using System.Web.Mvc;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Services;
using static szalkszop.Core.Models.ApplicationUser;
using static szalkszop.Core.Models.Order;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class OrderController : Controller
	{
		
		private readonly IOrderService _orderService;
		private readonly IPaymentMethodService _paymentMethodService;
		private readonly IDeliveryTypeService _deliveryTypeService;

		public OrderController(IOrderService orderService, IPaymentMethodService paymentMethodService, IDeliveryTypeService deliveryTypeService)
		{
			_orderService = orderService;
			_paymentMethodService = paymentMethodService;
			_deliveryTypeService = deliveryTypeService;
		}

		public ActionResult Index()
		{
			var viewModel = new AdminOrderViewModel
			{
				Orders = _orderService.GetOrderList(),
			};

			return View(viewModel);
		}

		public ActionResult Details(int orderId)
		{
			var viewModel = _orderService.GetOrderItemList(orderId);
			return View(viewModel);
		}

		public ActionResult Update(AdminOrderViewModel viewModel)
		{
			_orderService.UpdateStatus(viewModel.OrderId, viewModel.Status);

			return RedirectToAction("Index");
		}
	}
}