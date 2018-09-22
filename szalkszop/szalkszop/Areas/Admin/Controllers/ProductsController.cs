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
		public ActionResult Search(ProductSearchViewModel searchModel)
		{
			var viewModel = _productService.GetQueriedProductSearchViewModel(searchModel);

			return View("Index", viewModel);
		}

		public ActionResult Index()
		{
			var viewModel = _productService.GetProductsViewModel();

			return View(viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = _productService.AddProductViewModel();

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(ProductViewModel viewModel)
		{
			_productService.AddProduct(viewModel);

			return RedirectToAction("Index", "Products");
		}

		public ActionResult Edit(int id)
		{
			if (!_productService.ProductExist(id))
				return HttpNotFound();

			var viewModel = _productService.EditProductViewModel(id);

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductViewModel viewModel)
		{
			if (!_productService.ProductExist(viewModel.Id))
				return HttpNotFound();

			_productService.EditProduct(viewModel);

			return RedirectToAction("Index", "Products");
		}

		public ActionResult Delete(int id)
		{
			if (!_productService.ProductExist(id))
				return HttpNotFound();

			_productService.DeleteProduct(id);

			return RedirectToAction("Index", "Products");
		}
	}
}