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
			var viewModel = _productCategoryService.GetPartialCategoryView();

			return View("PartialCategories", viewModel);
		}

		public ActionResult ProductSearch()
		{
			var viewModel = _productService.GetProductSearchViewModel();

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult ProductSearch(ProductSearchModel searchModel)
		{
			var viewModel = _productService.GetQueriedProductSearchViewModel(searchModel);

			return View("Products", viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = _productService.GetProductViewModel();

			return View(viewModel);
		}

		public ActionResult ProductCategories()
		{
			var viewModel = _productCategoryService.GetProductCategorySearchResultViewModel();

			return View(viewModel);
		}

		public ActionResult ShowCategories()
		{
			var viewModel = _productCategoryService.GetProductCategoryViewModel();

			return View("LeftPanel", viewModel);
		}

		public ActionResult ShowProductInCategory(int id)
		{
			var viewModel = _productService.GetShowProductInCategoryViewModel(id);

			return View("Products", viewModel);
		}
	}
}