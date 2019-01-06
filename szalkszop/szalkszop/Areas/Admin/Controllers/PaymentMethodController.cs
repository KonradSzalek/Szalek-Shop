using System.Web.Mvc;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Services;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class PaymentMethodController : Controller
	{
		private readonly IPaymentMethodService _paymentMethodService;

		public PaymentMethodController(PaymentMethodService paymentMethodService)
		{
			_paymentMethodService = paymentMethodService;
		}

		public ActionResult Index()
		{
			var viewModel = _paymentMethodService.GetList();

			return View(viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new PaymentMethodViewModel
			{
				Heading = "Create",
			};

			return View("PaymentMethodForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create([Bind(Exclude = "Id")]PaymentMethodViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Create";

				return View("PaymentMethodForm", viewModel);
			}

			_paymentMethodService.AddPaymentMethod(viewModel);

			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			if (!_paymentMethodService.DoesPaymentMethodExist(id))
				return HttpNotFound();

			var paymentMethod = _paymentMethodService.GetPaymentMethod(id);

			var viewModel = new PaymentMethodViewModel
			{
				Heading = "Edit",
				Id = paymentMethod.Id,
				Cost = paymentMethod.Cost,
				Name = paymentMethod.Name,
			};

			return View("PaymentMethodForm", viewModel);
		}

		[HttpPost]
		public ActionResult Edit(PaymentMethodViewModel viewModel)
		{
			if (!_paymentMethodService.DoesPaymentMethodExist(viewModel.Id))
				return HttpNotFound();

			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Edit";
				return View("PaymentMethodForm", viewModel);
			}

			_paymentMethodService.Edit(viewModel);

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			if (!_paymentMethodService.DoesPaymentMethodExist(id))
				return HttpNotFound();

			_paymentMethodService.DeletePaymentMethod(id);

			return RedirectToAction("Index");
		}
	}
}