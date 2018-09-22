using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{

        // cr1 kompletnie zle rozdzielasz viewmodele -> wiecej powiem na zywo
        // kazdy widok ma miec wlasny viewmodel
        // nie mozesz reuzywasz viewmodeli do roznych widokow ktore robia co innego
		private readonly IProductCategoryService _productCategoryService;

		public ProductCategoryController(IProductCategoryService productCategoryService)
		{
			_productCategoryService = productCategoryService;
		}

		public ActionResult Index()
		{
			var viewModel = _productCategoryService.GetProductCategoryViewModel();

			return View(viewModel);
		}

		public ActionResult CreateCategory()
		{
			var viewModel = _productCategoryService.AddProductCategoryViewModel(); // cr3 sam powinienes tworzyc ten viewmodel w kontrolerze

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateCategory(ProductCategoryViewModel viewModel)
		{
			_productCategoryService.AddProductCategory(viewModel);

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult DeleteCategory(int id)
		{
			if (!_productCategoryService.IsProductCategoryExist(id))
				return HttpNotFound();

			_productCategoryService.DeleteProductCategory(id);

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult EditCategory(int id)
		{

			if (!_productCategoryService.IsProductCategoryExist(id))
				return HttpNotFound();

			var viewModel = _productCategoryService.EditProductCategoryViewModel(id);

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCategory(ProductCategoryViewModel viewModel)
		{
			if (!_productCategoryService.IsProductCategoryExist(viewModel.Id))
				return HttpNotFound();

			_productCategoryService.EditProductCategory(viewModel);

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}