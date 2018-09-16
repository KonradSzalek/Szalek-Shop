using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
        // cr2 injectuj przez interfejsy
		private readonly ProductCategoryService _productCategoryService;
		private readonly ProductService _productService;

		public ProductCategoryController(ProductCategoryService productCategoryService, ProductService productService)

		{
			_productCategoryService = productCategoryService;
			_productService = productService;
		}

		public ActionResult Index()
		{
			var products = _productService.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _productCategoryService.GetCategoriesWithAmountOfProducts(products),
			};

			return View(viewModel);
		}

		public ActionResult CreateCategory()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Add a new category",
			};
			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateCategory(ProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_productCategoryService.Add(category);
            // cr2 nie uzywaj complete w kontrolerze
			_productCategoryService.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult DeleteCategory(int id)
		{
            // cr2 sprawdz najpierw czy taka kategoria istnieje i jezeli nie redirectuj do listy kategorii z info ze nie znaleziono kategorii
			var category = _productCategoryService.GetEditingProductCategory(id);

			_productCategoryService.Remove(category);

            // cr2 nie uzywaj compelete na poziomie kontrolera
			_productCategoryService.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult EditCategory(int id)
		{
            // cr2 w tej metodzie powinienes najpierw sprawdzic czy taka kategoria w ogole istnieje i jezeli nie to
            // najlepiej redirectowac do strony glownej edycji kategorii i wyswietlic blad gdzies u gory ze nie znaleziono takiej kategorii
            // polecam wyswietlic cos takiego jak bootstrap alert
            // zeby decydowac kiedy go wyswietlic na stronie mozesz uzyc czegos takiego Jak TempData
            // tempdata jest dostepne z poziomu kontrolera i widoku i mozesz tam wrzucac pewne dane, np. czy pokazac jakis error
			var category = _productCategoryService.GetEditingProductCategoryDto(id);

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Update Category",
				Name = category.Name,
			};

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        // cr2 zmien nazwe na editCategory
        // cr2 ogolnie cala metoda jest napisana zle
        // na poscie powinienes najpierw upewnic sie ze taka kategoria w ogole istnieje
        // jezeli nie, to to samo co wyzej, redirect do strony glownej edycji kategorii z wyswietlonym bledem
		public ActionResult UpdateCategory(ProductCategoryViewModel viewModel)
		{
			var category = _productCategoryService.GetEditingProductCategory(viewModel.Id);

			{ // cr2 od czego sa te 2 klamry XDDD
				category.Name = viewModel.Name;
			} // od czego sa te 2 klamry XDDD

            // cr2 nie uzywaj complete w kontrolerach

			_productCategoryService.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}