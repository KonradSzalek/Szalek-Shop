using System.Web.Mvc;
using szalkszop.Services;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;

		public ProductCategoryController(IProductCategoryService productCategoryService)
		{
			_productCategoryService = productCategoryService;
		}

		public ActionResult Index()
		{
			var viewModel = _productCategoryService.GetProductCategoryWithProductCountList();

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
			if (!_productCategoryService.DoesProductCategoryExist(id))
				return HttpNotFound();

			_productCategoryService.DeleteProductCategory(id);

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult Edit(int id)
		{
			if (!_productCategoryService.DoesProductCategoryExist(id))
				return HttpNotFound();

			var viewModel = _productCategoryService.EditProductCategory(id);
			viewModel.Heading = "Update Category";

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(AdminProductCategoryViewModel viewModel)
		{
			if (!_productCategoryService.DoesProductCategoryExist(viewModel.Id))
				return HttpNotFound();

			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			_productCategoryService.EditProductCategory(viewModel);

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}