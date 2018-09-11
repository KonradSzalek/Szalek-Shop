using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Persistance;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductCategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductCategoryController(IUnitOfWork unitOfWork)

		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult ProductCategories()
		{
			var products = _unitOfWork.Products.GetProductsWithCategory().ToList();

			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Product Categories",
				ProductCategories = _unitOfWork.ProductCategories.GetCategoriesWithAmountOfProducts(products),
			};

			return View(viewModel);
		}

		public ActionResult NewCategory()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Add a new category",
			};
			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		public ActionResult NewCategory(ProductCategoryViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_unitOfWork.ProductCategories.Add(category);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult RemoveCategory(int id)
		{
			var category = _unitOfWork.ProductCategories.GetEditingProductCategory(id);

			_unitOfWork.ProductCategories.Remove(category);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult EditCategory(int id)
		{
			var category = _unitOfWork.ProductCategories.GetEditingProductCategory(id);

			var viewModel = new ProductCategoryViewModel
			{
				Name = category.Name,
			};

			return View("CategoryForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateCategory(ProductCategoryViewModel viewModel)
		{
			var category = _unitOfWork.ProductCategories.GetEditingProductCategory(viewModel.Id);

			{
				category.Name = viewModel.Name;
			}

			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}
	}
}