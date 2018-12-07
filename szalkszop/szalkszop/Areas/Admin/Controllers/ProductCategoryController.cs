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
			var viewModel = _productCategoryService.GetAdminProductCategoriesViewModel();

			return View(viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new AdminProductCategoryViewModel
			{
				Heading = "Add category",
			};

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create([Bind(Exclude = "Id")] AdminProductCategoryViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Add category";
				return View("CategoryForm", viewModel);
			}

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
			viewModel.Heading = "Update Category";

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(AdminProductCategoryViewModel viewModel)
		{
            //CR5 ModelState.IsValid
            if (!_productCategoryService.ProductCategoryExist(viewModel.Id))
				return HttpNotFound();

			_productCategoryService.EditProductCategory(viewModel);

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}