using System.Web.Mvc;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Services;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class DeliveryTypeController : Controller
	{
		private readonly IDeliveryTypeService _deliveryTypeService;

		public DeliveryTypeController(DeliveryTypeService deliveryTypeService)
		{
			_deliveryTypeService = deliveryTypeService;
		}

		public ActionResult Index()
		{
			var viewModel = _deliveryTypeService.GetList();

			return View(viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new DeliveryTypeViewModel
			{
				Heading = "Create",
			};

			return View("DeliveryTypeForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create([Bind(Exclude = "Id")] DeliveryTypeViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Create";

				return View("DeliveryTypeForm", viewModel);
			}

			_deliveryTypeService.AddDeliveryType(viewModel);

			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			if (!_deliveryTypeService.DoesDeliveryTypeExist(id))
				return HttpNotFound();

			var deliveryType = _deliveryTypeService.GetDeliveryType(id);

			var viewModel = new DeliveryTypeViewModel
			{
				Heading = "Edit",
				Name = deliveryType.Name,
				Id = deliveryType.Id,
				Cost = deliveryType.Cost,
			};

			return View("DeliveryTypeForm", viewModel);
		}

		[HttpPost]
		public ActionResult Edit(DeliveryTypeViewModel viewModel)
		{
			if (!_deliveryTypeService.DoesDeliveryTypeExist(viewModel.Id))
				return HttpNotFound();

			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Edit";
				return View("DeliveryTypeForm", viewModel);
			}

			_deliveryTypeService.Edit(viewModel);

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			if (!_deliveryTypeService.DoesDeliveryTypeExist(id))
				return HttpNotFound();

			_deliveryTypeService.DeleteDeliveryType(id);

			return RedirectToAction("Index");
		}
	}
}