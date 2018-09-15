using System.Linq;
using System.Web.Mvc;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductRepository _productRepository;

		public HomeController(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)

		{
			_productCategoryRepository = productCategoryRepository;
			_productRepository = productRepository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult TopThreeProducts()
		{
			var viewModel = new ProductViewModel
			{
				Products = _productRepository.GetThreeNewestProducts(),
			};

			return View("TopThreeProductsPartial", viewModel);
		}

		public ActionResult PartialCategories()
		{
			var products = _productRepository.GetProductsWithCategory().ToList();

			var viewModel = new ProductViewModel
			{
				ProductCategories = _productCategoryRepository.GetCategoriesWithAmountOfProducts(products),
			};

			return View("PartialCategories", viewModel);
		}

		public ActionResult ProductSearch()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _productCategoryRepository.GetProductCategories()
			};

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult ProductSearch(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = _productRepository.
					GetQueriedProducts(searchModel, _productRepository.GetProductsWithCategory()),
			};

			return View("Products", viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = _productRepository.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		public ActionResult ProductCategories()
		{
			var products = _productRepository.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _productCategoryRepository.GetCategoriesWithAmountOfProducts(products),
			};

			return View(viewModel);
		}

		public ActionResult ShowCategories()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategories = _productCategoryRepository.GetProductCategories(),
			};

			return View("LeftPanel", viewModel);
		}

		public ActionResult ShowProductInCategory(int id)
		{
			var viewModel = new ProductViewModel
			{
				Products = _productRepository.GetProductInCategory(id)
			};

			return View("Products", viewModel);
		}
	}
}