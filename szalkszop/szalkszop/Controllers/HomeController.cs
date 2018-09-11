using System.Linq;
using System.Web.Mvc;
using szalkszop.Persistance;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(IUnitOfWork unitOfWork)

		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult TopThreeProducts()
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.GetThreeNewestProducts(),
			};

			return View("TopThreeProductsPartial", viewModel);
		}

		public ActionResult PartialCategories()
		{
			var products = _unitOfWork.Products.GetProductsWithCategory().ToList();

			var viewModel = new ProductViewModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetCategoriesWithAmountOfProducts(products),
			};

			return View("PartialCategories", viewModel);
		}

		public ActionResult ProductSearch()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories()
			};

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult ProductSearch(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.
					GetQueriedProducts(searchModel, _unitOfWork.Products.GetProductsWithCategory()),
			};

			return View("Products", viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = _unitOfWork.Products.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		public ActionResult ProductCategories()
		{
			var products = _unitOfWork.Products.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _unitOfWork.ProductCategories.GetCategoriesWithAmountOfProducts(products),
			};

			return View(viewModel);
		}

		public ActionResult ShowCategories()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
			};

			return View("LeftPanel", viewModel);
		}

		public ActionResult ShowProductInCategory(int id)
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.GetProductInCategory(id)
			};

			return View("Products", viewModel);
		}
	}
}