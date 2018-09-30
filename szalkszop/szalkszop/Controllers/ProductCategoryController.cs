using System.Web.Mvc;
using szalkszop.Services;

namespace szalkszop.Controllers
{
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;
		private readonly IProductService _productService;

		public ProductCategoryController(ProductCategoryService productCategoryService, ProductService productService)
		{
			_productCategoryService = productCategoryService;
			_productService = productService;
		}

		public ActionResult Index()
		{
			// ustawić heading view wspolny
			var viewModel = _productCategoryService.GetProductCategoriesWithProductCountViewModel();

			return View(viewModel);
		}

		public ActionResult ShowCategories()
		{
			// ustawic heading view wspolny

			var viewModel = _productCategoryService.GetProductCategoriesViewModel();

			return View("_LeftPanel", viewModel);
		}

		public ActionResult GetCategories()
		{
			var viewModel = _productCategoryService.GetProductCategoriesWithProductCountViewModel();

			return View("_PartialCategories", viewModel);
		}

		public ActionResult Products(int categoryId)
		{
			var viewModel = _productService.GetProductsByCategoryViewModel(categoryId);

			return View("~/Views/Product/Products.cshtml", viewModel);
		}

	}
}