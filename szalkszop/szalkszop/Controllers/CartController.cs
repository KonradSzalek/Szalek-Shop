using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class CartController : Controller
	{
		private readonly IProductService _productService;

		public CartController(IProductService productService)
		{
			_productService = productService;
		}

		public ActionResult Index()
		{
			var userId = User.Identity.GetUserId();

			var viewModel = new CartViewModel
			{
				ItemList = (List<Item>)Session["cart" + userId],
				UserId = userId,
			};

			return View(viewModel);
		}

		public ActionResult CartDropdownList()
		{
			var userId = User.Identity.GetUserId();

			var viewModel = new CartViewModel
			{
				ItemList = (List<Item>)Session["cart" + userId],
				UserId = userId,
			};

			return View(viewModel);
		}

		public ActionResult Buy(int id)
		{
			var userId = User.Identity.GetUserId();

			if (System.Web.HttpContext.Current.Session["cart" + userId] == null)
			{
				List<Item> cart = new List<Item>();
				cart.Add(new Item { Product = _productService.GetProduct(id), Quantity = 1 });
				System.Web.HttpContext.Current.Session["cart" + userId] = cart;
			}
			else
			{
				List<Item> cart = (List<Item>)System.Web.HttpContext.Current.Session["cart" + userId];
				int index = DoesExist(id);
				if (index != -1)
				{
					cart[index].Quantity++;
				}
				else
				{
					cart.Add(new Item { Product = _productService.GetProduct(id), Quantity = 1 });
				}
				System.Web.HttpContext.Current.Session["cart" + userId] = cart;
			}
			return RedirectToAction("Index", "Product");
		}

		public ActionResult RemoveSingle(int id)
		{
			var userId = User.Identity.GetUserId();

			List<Item> cart = (List<Item>)System.Web.HttpContext.Current.Session["cart" + userId];

			int index = DoesExist(id);

			if (index != -1)
			{
				if (cart[index].Quantity > 1)
				{
					cart[index].Quantity--;
				}

				else
				{
					cart.RemoveAt(index);
				}
			}
	
			System.Web.HttpContext.Current.Session["cart" + userId] = cart;

			return RedirectToAction("Index", "Product");
		}

		public ActionResult Remove(int id)
		{
			var userId = User.Identity.GetUserId();

			List<Item> cart = (List<Item>)Session["cart" + userId];
			int index = DoesExist(id);
			cart.RemoveAt(index);
			Session["cart" + userId] = cart;

			return RedirectToAction("Index", "Product");
		}

		private int DoesExist(int id)
		{
			var userId = User.Identity.GetUserId();

			List<Item> cart = (List<Item>)Session["cart" + userId];
			for (int i = 0; i < cart.Count; i++)
				if (cart[i].Product.Id.Equals(id))
					return i;
			return -1;
		}

	}
}
