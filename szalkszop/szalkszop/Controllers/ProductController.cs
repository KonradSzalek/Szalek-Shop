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
			
			return View("_Products", viewModel);
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

		[HttpPost]
		public ActionResult Search(ProductListSearchViewModel searchModel)
		{
			ModelState.Remove("ProductSearchViewModel.ProductCategory.Id");
			if (!ModelState.IsValid)
			{
				return View("SearchResult", searchModel);
			}
			// CR5FIXED dodac modelstate.IsValid
            
			var viewModel = new ProductListSearchViewModel
			{
				ProductSearchResultList = _productService.GetQueriedProductList(searchModel.ProductFiltersViewModel),
				ProductFiltersViewModel = _productService.GetProductSearch(),
			};

			return View("SearchResult", viewModel);
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