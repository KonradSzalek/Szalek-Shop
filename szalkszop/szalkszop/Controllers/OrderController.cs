using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	[Authorize]
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
			bool orderMade = false;

			if (!ModelState.IsValid)
			{
				viewModel.PaymentMethods = _paymentMethodService.GetList();
				viewModel.DeliveryTypes = _deliveryTypeService.GetList();
				return View(viewModel);
			}

			viewModel.OrderedItemList = (List<Item>)System.Web.HttpContext.Current.Session["cart" + userId];

			if (viewModel.OrderedItemList?.Any() ?? false)
			{
				_orderService.CompleteOrder(viewModel, userId);
				orderMade = true;
				Session.Remove("cart" + userId);
				System.Web.HttpContext.Current.Session["orderMade" + userId] = orderMade;
			}

			return RedirectToAction("MyOrders", "Order");
		}

		public ActionResult CancelOrder(int id)
		{
			if (!_orderService.DoesOrderExist(id))
				return HttpNotFound();

			if (_orderService.IsUserAuthorized(id) != User.Identity.GetUserId())
			{
				return new HttpUnauthorizedResult();
			}

			else
			{
				_orderService.Cancel(id);
				return RedirectToAction("MyOrders");
			}
		}

		public ActionResult MyOrders()
		{
			var userId = User.Identity.GetUserId();
			bool orderMade = false;

			if (System.Web.HttpContext.Current.Session["orderMade" + userId] != null)
			{
				orderMade = (bool)System.Web.HttpContext.Current.Session["orderMade" + userId];
			}

			var viewModel = new MyOrdersViewModel
			{
				Orders = _orderService.GetUserOrderList(User.Identity.GetUserId()),
				OrderMade = orderMade,
			};

			Session.Remove("orderMade" + userId);

			return View(viewModel);
		}

		public ActionResult Details(int id)
		{
			if (!_orderService.DoesOrderExist(id))
				return HttpNotFound();

			if (_orderService.IsUserAuthorized(id) != User.Identity.GetUserId())
			{
				return new HttpUnauthorizedResult();
			};

			var viewModel = new OrderDetailsViewModel
			{
				OrderItems = _orderService.GetOrderItemList(id),
				Order = _orderService.GetOrder(id),
			};

			return View(viewModel);
		}
	}
}