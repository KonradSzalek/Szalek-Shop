using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
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
			var viewModel = _productCategoryService.AddProductCategoryViewModel();

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
			// cr2 w tej metodzie powinienes najpierw sprawdzic czy taka kategoria w ogole istnieje i jezeli nie to
			// najlepiej redirectowac do strony glownej edycji kategorii i wyswietlic blad gdzies u gory ze nie znaleziono takiej kategorii
			// polecam wyswietlic cos takiego jak bootstrap alert
			// zeby decydowac kiedy go wyswietlic na stronie mozesz uzyc czegos takiego Jak TempData
			// tempdata jest dostepne z poziomu kontrolera i widoku i mozesz tam wrzucac pewne dane, np. czy pokazac jakis error

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