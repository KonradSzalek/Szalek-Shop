using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;

		public ProductCategoryController(IProductCategoryService productCategoryService)
		{
			_productCategoryService = productCategoryService;
		}

		public ActionResult Index()
		{
			var viewModel = _productCategoryService.GetProductCategoriesWithProductCountViewModel();

			return View(viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Add a new category",
			};

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(ProductCategoryViewModel viewModel)
		{
			_productCategoryService.AddProductCategory(viewModel);

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult Delete(int id)
		{
			if (!_productCategoryService.ProductCategoryExist(id))
				return HttpNotFound();

			_productCategoryService.DeleteProductCategory(id);

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult Edit(int id)
		{
			if (!_productCategoryService.ProductCategoryExist(id))
				return HttpNotFound();

			var viewModel = _productCategoryService.EditProductCategoryViewModel(id);

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductCategoryViewModel viewModel)
		{
			if (!_productCategoryService.ProductCategoryExist(viewModel.Id))
				return HttpNotFound();

			_productCategoryService.EditProductCategory(viewModel);

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}