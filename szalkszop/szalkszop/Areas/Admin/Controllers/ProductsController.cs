using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		public ActionResult Search()
		{
			var viewModel = _productService.GetProductSearchViewModel();

			return View("ProductSearch", viewModel);
		}
        
		[HttpPost]
		public ActionResult Search(ProductSearchModel searchModel)
		{
			var viewModel = _productService.GetQueriedProductSearchViewModel(searchModel);

			return View("Index", viewModel);
		}

		public ActionResult Index()
		{
			var viewModel = _productService.GetProductViewModel();

			return View(viewModel);
		}

		public ActionResult CreateProduct()
		{
			var viewModel = _productService.AddProductViewModel();

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateProduct(ProductViewModel viewModel)
		{
			_productService.AddProduct(viewModel);

			return RedirectToAction("Index", "Products");
		}

		public ActionResult EditProduct(int id)
		{
			if (!_productService.IsProductExist(id))
				return HttpNotFound();

			var viewModel = _productService.EditProductViewModel(id);

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProduct(ProductViewModel viewModel)
		{
			if (!_productService.IsProductExist(viewModel.Id))
				return HttpNotFound();

			_productService.EditProduct(viewModel);

			return RedirectToAction("Index", "Products");
		}

		public ActionResult DeleteProduct(int id)
		{
			if (!_productService.IsProductExist(id))
				return HttpNotFound();

			_productService.DeleteProduct(id);

			return RedirectToAction("Index", "Products");
		}
	}
}