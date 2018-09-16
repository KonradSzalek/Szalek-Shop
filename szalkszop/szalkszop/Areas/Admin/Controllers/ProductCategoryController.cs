using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
		private readonly ProductCategoryService _productCategoryService;
		private readonly ProductService _productService;

		public ProductCategoryController(ProductCategoryService productCategoryService, ProductService productService)

		{
			_productCategoryService = productCategoryService;
			_productService = productService;
		}

		public ActionResult Index()
		{
			var products = _productService.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _productCategoryService.GetCategoriesWithAmountOfProducts(products),
			};

			return View(viewModel);
		}

		public ActionResult CreateCategory()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Add a new category",
			};
			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateCategory(ProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_productCategoryService.Add(category);
			_productCategoryService.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult DeleteCategory(int id)
		{
			var category = _productCategoryService.GetEditingProductCategory(id);

			_productCategoryService.Remove(category);
			_productCategoryService.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult EditCategory(int id)
		{
			var category = _productCategoryService.GetEditingProductCategoryDto(id);

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Update Category",
				Name = category.Name,
			};

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateCategory(ProductCategoryViewModel viewModel)
		{
			var category = _productCategoryService.GetEditingProductCategory(viewModel.Id);

			{
				category.Name = viewModel.Name;
			}

			_productCategoryService.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}