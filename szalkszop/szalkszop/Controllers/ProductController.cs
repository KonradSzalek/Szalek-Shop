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
			var viewModel = new ProductsWithSearchViewModel
			{
				ProductsDto = _productService.GetThreeNewestProducts(),
			};
			
			return View("_Products", viewModel);
		}

		public ActionResult Index()
		{
			var viewModel = new ProductsWithSearchViewModel
			{
				ProductsDto = _productService.GetProducts(),
				ProductSearchViewModel = _productService.GetProductSearchViewModel(),
			};
			
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(ProductsWithSearchViewModel searchModel)
		{
			// dodac modelstate.IsValid
            //CR5
			var viewModel = new ProductsWithSearchViewModel
			{
				ProductSearchResult = _productService.GetQueriedProducts(searchModel.ProductSearchViewModel),
				ProductSearchViewModel = _productService.GetProductSearchViewModel(),
			};

			return View("SearchResult", viewModel);
		}

		public ActionResult Details(int id)
		{
			if (!_productService.ProductExist(id))
				return HttpNotFound();

			var viewModel = _productService.ProductDetailViewModel(id);
			return View(viewModel);
		}
	}
}