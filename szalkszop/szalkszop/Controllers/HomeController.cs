using System.Web.Mvc;
using szalkszop.Services;
using szalkszop.ViewModels;
// cr4 uwagi ogolne do calego kodu:
// 1. Wszystkie viewmodele masz w folderze viewmodels/, przenies viewmodele ktore sa w admin area do folderu Areas/Admin/Viewmodels
//      Te, ktore sa uzyte i tu i tu moga byc w viewmodels/
// 2. Poprzenos tworzenie niektorych viewmodelow ktore maja wypelniany heading do kontrolerow, przyklady:
// dla takiego viewmodelu:
/*
 * SomeViewMode{
 *  Heading,
 *  Categoires
 * }
 * 
 * w kontrolerze mozesz miec wtedy:
 * new SomeViewModel { 
 *  Heading = "SomeHeading", 
 *  Categories = service.GetCategories()
 * }
 * 
 * Czyli serwis zwraca sama liste a nie caly viewmodel - nie musi wiedziec o rzeczach zwiazanych z widokiem
 * 
 * Jezeli Viewmodel ma kilka propertasow czyli np. 1
 * new SomeViewModel { 
 *  Heading,
 *  Dupa,
 *  Cipa
 * }
 * zwracaj ten viewmodel serwisem, ale wypelniaj heading w kontrolerze
 * Sprawa sie tyczy nie tylko headingow ale jakiejkolwiek wartosci ktora jest przydatna JEDYNIE w kontrolerze/widoku (wtedy serwis nie powinien o niej w ogole wiedziec)
 * 3. Zle nazywasz metody serwisów, i property  na viewmodelach. Nie nazywaj ich CategoriesDto albo GetPRoductViewModel
 * Wystarczy Categories, albo GetProduct (jak ktos bedzie potrzebowal specyficznego info to spojrzy na typ)
 * 4. Jak serwis zwraca viewmodel ktory ma w sobie tylko liste, to wywal tworzenie viewmodelu z serwisu, zmien metode by zwracala tylko liste i zmien jej nazwie
 * hint: jezeli w nazwie serwisu jest viewmodel, najczesciej zrobiles cos zle
 */

namespace szalkszop.Controllers
{
    // cr4 rozdziel homeController na oddzielne kontrollery home, categories, products etc.
	public class HomeController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;
		private readonly IProductService _productService;

		public HomeController(ProductCategoryService productCategoryService, ProductService productService)
		{
			_productCategoryService = productCategoryService;
			_productService = productService;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult TopThreeProducts()
		{
			var viewModel = _productService.GetThreeNewestProductsViewModel();

			return View("TopThreeProductsPartial", viewModel);
		}

        // cr4 nie nazywaj tak metod, to zalezy od konkretnego uzycia czy dana akcja kontrolera bedzie w partialu czy nie
        // wystarczy samo GetCategories
		public ActionResult PartialCategories()
		{
			var viewModel = _productCategoryService.GetProductCategoriesWithProductCountViewModel();

			return View("PartialCategories", viewModel);
		}

		public ActionResult ProductSearch()
		{
			var viewModel = _productService.GetProductSearchViewModel();

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult ProductSearch(ProductSearchViewModel searchModel)
		{
			var viewModel = _productService.GetQueriedProductSearchViewModel(searchModel);

			return View("Products", viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = _productService.GetProductsViewModel();

			return View(viewModel);
		}

		public ActionResult ProductCategories()
		{
			// ustawić heading view wspolny
			var viewModel = _productCategoryService.GetProductCategoriesWithProductCountViewModel();

			return View(viewModel);
		}

		public ActionResult ShowCategories()
		{
			// ustawic heading view wspolny

			var viewModel = _productCategoryService.GetProductCategoriesViewModel();

			return View("LeftPanel", viewModel);
		}

		public ActionResult ShowProductInCategory(int categoryId)
		{
			var viewModel = _productService.GetProductsByCategoryViewModel(categoryId);

			return View("Products", viewModel);
		}
	}
}