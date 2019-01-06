using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class OrderController : Controller
	{
		private readonly IUserService _userService;
		private readonly IOrderService _orderService;
		private readonly IPaymentMethodService _paymentMethodService;
		private readonly IDeliveryTypeService _deliveryTypeService;

		public OrderController(
			UserService userService, 
			OrderService orderService, 
			PaymentMethodService paymentMethodService, 
			DeliveryTypeService deliveryTypeService)
		{
			_userService = userService;
			_orderService = orderService;
			_paymentMethodService = paymentMethodService;
			_deliveryTypeService = deliveryTypeService;
		}

		public ActionResult MakeOrder()
		{
			var userId = User.Identity.GetUserId();

			var viewModel = new CreateOrderViewModel
			{
				UserContactDetails = _userService.GetUserContactDetails(userId),
				PaymentMethods = _paymentMethodService.GetList(),
				DeliveryTypes = _deliveryTypeService.GetList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult MakeOrder(CreateOrderViewModel viewModel)
		{
			var userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				viewModel.PaymentMethods = _paymentMethodService.GetList();
				viewModel.DeliveryTypes = _deliveryTypeService.GetList();
				return View(viewModel);
			}

			viewModel.OrderedItemList = (List<Item>)System.Web.HttpContext.Current.Session["cart" + userId];

			_orderService.CompleteOrder(viewModel, userId);

			Session.Remove("cart" + userId);

			return RedirectToAction("Index", "Home");
		}

		public ActionResult MyOrders()
		{
			var userId = User.Identity.GetUserId();
			var viewModel = _orderService.GetUserOrderList(User.Identity.GetUserId());
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
	}
}