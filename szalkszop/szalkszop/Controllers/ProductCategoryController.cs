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
			var viewModel = _productCategoryService.GetPopulatedOnlyProductCategoryList();

			return View(viewModel);
		}

		public ActionResult Categories()
		{
			var viewModel = _productCategoryService.GetPopulatedOnlyProductCategoryList();

			return View("_PartialCategories", viewModel);
		}

		public ActionResult Products(int categoryId)
		{
			if (!_productCategoryService.DoesProductCategoryExist(categoryId))
				return HttpNotFound();

			var viewModel = _productService.GetProductListByCategory(categoryId);

			return View("~/Views/Product/Products.cshtml", viewModel);
		}

		public ActionResult LeftPanel()
		{
			var viewModel = _productCategoryService.GetPopulatedOnlyProductCategoryList();

			return View("~/Views/Shared/_LeftPanel.cshtml", viewModel);
		}
	}
}