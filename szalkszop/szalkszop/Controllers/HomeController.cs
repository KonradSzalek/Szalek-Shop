using System;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Persistance;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(IUnitOfWork unitOfWork)

		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.GetThreeNewestProducts(),
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
			};

			return View(viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		[Authorize]
		public ActionResult NewProduct()
		{
			var viewModel = new ProductViewModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
				Heading = "Add a product",
			};

			return View("ProductForm", viewModel);
		}

		[Authorize]
		[HttpPost]
		public ActionResult NewProduct(ProductViewModel viewModel)
		{
			var product = new Product
			{
				ProductCategoryId = viewModel.ProductCategory,
				Name = viewModel.Name,
				DateOfAdding = DateTime.Now,
			};

			_unitOfWork.Products.Add(product);

			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult ProductCategories()
		{
			var viewModel = new ProductViewModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
			};

			return View(viewModel);
		}

		[Authorize]
		public ActionResult EditProduct(int id)
		{
			var product = _unitOfWork.Products.GetEditingProduct(id);

			var viewModel = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
				ProductCategory = product.ProductCategoryId,
				Heading = "Edit a product",
			};

			return View("ProductForm", viewModel);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateProduct(ProductViewModel viewModel)
		{
			var product = _unitOfWork.Products.GetEditingProduct(viewModel.Id);

			{
				product.Name = viewModel.Name;
				product.ProductCategoryId = viewModel.ProductCategory;
			}

			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult NewProductCategory()
		{
			return View();
		}

		[Authorize]
		public ActionResult NewCategory()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Add a new category",
			};
			return View("CategoryForm", viewModel);
		}

		[Authorize]
		[HttpPost]
		public ActionResult NewCategory(ProductViewModel viewModel)
		{
			var category = new ProductCategory
			{
				Name = viewModel.Name
			};

			_unitOfWork.ProductCategories.Add(category);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult RemoveCategory(int id)
		{
			var category = _unitOfWork.ProductCategories.GetEditingProductCategory(id);

			_unitOfWork.ProductCategories.Remove(category);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}
	}
}