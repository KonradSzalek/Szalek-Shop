using System.Web.Mvc;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(ProductService productService)
		{
			_productService = productService;
		}

		public ActionResult TopThreeProducts()
		{
			var viewModel = new ProductListSearchViewModel
			{
				ProductList = _productService.GetThreeNewestProducts(),
			};

			return View("_ProductTopThree", viewModel);
		}

		public ActionResult Index()
		{
			var viewModel = new ProductListSearchViewModel
			{
				ProductList = _productService.GetProductList(),
				ProductFiltersViewModel = _productService.GetProductSearch(),
			};

			return View(viewModel);
		}

		public ActionResult Details(int id)
		{
			if (!_productService.DoesProductExist(id))
				return HttpNotFound();

			var viewModel = _productService.GetProductDetail(id);
			return View(viewModel);
		}
	}
}