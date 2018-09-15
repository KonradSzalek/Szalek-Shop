using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductRepository _productRepository;

		public ProductCategoryController(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)

		{
			_productCategoryRepository = productCategoryRepository;
			_productRepository = productRepository;
		}

		public ActionResult Index()
		{
			var products = _productRepository.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _productCategoryRepository.GetCategoriesWithAmountOfProducts(products),
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

			_productCategoryRepository.Add(category);
			_productCategoryRepository.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult DeleteCategory(int id)
		{
			var category = _productCategoryRepository.GetEditingProductCategory(id);

			_productCategoryRepository.Remove(category);
			_productCategoryRepository.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}

		public ActionResult EditCategory(int id)
		{
			var category = _productCategoryRepository.GetEditingProductCategory(id);

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
			var category = _productCategoryRepository.GetEditingProductCategory(viewModel.Id);

			{
				category.Name = viewModel.Name;
			}

			_productCategoryRepository.Complete();

			return RedirectToAction("Index", "ProductCategory");
		}
	}
}