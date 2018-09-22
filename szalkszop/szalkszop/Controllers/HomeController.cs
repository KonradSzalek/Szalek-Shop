using System.Web.Mvc;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
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