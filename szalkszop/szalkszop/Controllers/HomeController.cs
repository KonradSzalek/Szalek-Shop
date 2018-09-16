using System.Linq;
using System.Web.Mvc;
using szalkszop.Repositories;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ProductCategoryService _productCategoryService;
		private readonly ProductService _productService;

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
			var viewModel = new ProductViewModel
			{
				Products = _productService.GetThreeNewestProducts(),
			};

			return View("TopThreeProductsPartial", viewModel);
		}

		public ActionResult PartialCategories()
		{
			var products = _productService.GetProductsWithCategory().ToList();

			var viewModel = new ProductViewModel
			{
				ProductCategories = _productCategoryService.GetCategoriesWithAmountOfProducts(products),
			};

			return View("PartialCategories", viewModel);
		}

		public ActionResult ProductSearch()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _productCategoryService.GetProductCategories()
			};

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult ProductSearch(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = _productService.
					GetQueriedProducts(searchModel, _productService.GetProductsWithCategory()),
			};

			return View("Products", viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = _productService.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		public ActionResult ProductCategories()
		{
			var products = _productService.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _productCategoryService.GetCategoriesWithAmountOfProducts(products),
			};

			return View(viewModel);
		}

		public ActionResult ShowCategories()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategories = _productCategoryService.GetProductCategories(),
			};

			return View("LeftPanel", viewModel);
		}

		public ActionResult ShowProductInCategory(int id)
		{
			var viewModel = new ProductViewModel
			{
				Products = _productService.GetProductInCategory(id)
			};

			return View("Products", viewModel);
		}
	}
}