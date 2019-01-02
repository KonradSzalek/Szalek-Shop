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

		public ActionResult Edit(int id)
		{
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
			_deliveryTypeService.Edit(viewModel);

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			_deliveryTypeService.DeleteDeliveryType(id);

			return RedirectToAction("Index");
		}

		public ActionResult Create()
		{
			var viewModel = new DeliveryTypeViewModel();
			viewModel.Heading = "Create";
			
			return View("DeliveryTypeForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(DeliveryTypeViewModel viewModel)
		{
			_deliveryTypeService.AddDeliveryType(viewModel);

			return RedirectToAction("Index");
		}
	}
}