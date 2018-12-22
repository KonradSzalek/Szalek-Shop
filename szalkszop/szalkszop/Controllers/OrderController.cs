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

		public OrderController(UserService userService, OrderService orderService)
		{
			_userService = userService;
			_orderService = orderService;
		}

		public ActionResult MakeOrder()
		{
			var userId = User.Identity.GetUserId();

			var viewModel = new OrderViewModel
			{
				UserContactDetails = _userService.GetUserContactDetails(userId),
				PaymentMethods = _orderService.GetPaymentMethodList(),
				DeliveryTypes = _orderService.GetDeliveryTypeList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult MakeOrder(OrderViewModel viewModel)
		{
			var userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				viewModel.PaymentMethods = _orderService.GetPaymentMethodList();
				viewModel.DeliveryTypes = _orderService.GetDeliveryTypeList();
				return View(viewModel);
			}

			viewModel.OrderedItemList = (List<Item>)System.Web.HttpContext.Current.Session["cart" + userId];

			_orderService.CompleteOrder(viewModel);

			Session.Remove("cart" + userId);

			return RedirectToAction("Index", "Home");
		}
	}
}