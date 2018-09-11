using System;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Persistance;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductsController(IUnitOfWork unitOfWork)

		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult ProductSearch()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories()
			};

			return View("~/Views/Home/ProductSearch.cshtml", viewModel);
		}

		[HttpPost]
		public ActionResult ProductSearch(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.
					GetQueriedProducts(searchModel, _unitOfWork.Products.GetProductsWithCategory()),
			};

			return View("Products", viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = _unitOfWork.Products.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		public ActionResult NewProduct()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Add a product",
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
			};

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		public ActionResult NewProduct(ProductViewModel viewModel)
		{
			var product = new Product
			{
				ProductCategoryId = viewModel.ProductCategory,
				Name = viewModel.Name,
				AmountInStock = viewModel.AmountInStock,
				Price = viewModel.Price,
				Description = viewModel.Description,
				DateOfAdding = DateTime.Now,
			};

			_unitOfWork.Products.Add(product);

			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult EditProduct(int id)
		{
			var product = _unitOfWork.Products.GetEditingProduct(id);

			var viewModel = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
				ProductCategory = product.ProductCategoryId,
				AmountInStock = product.AmountInStock,
				Price = product.Price,
				Description = product.Description,
				Heading = "Edit a product",
			};

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateProduct(ProductViewModel viewModel)
		{
			var product = _unitOfWork.Products.GetEditingProduct(viewModel.Id);

			{
				product.Name = viewModel.Name;
				product.ProductCategoryId = viewModel.ProductCategory;
				product.AmountInStock = viewModel.AmountInStock;
				product.Price = viewModel.Price;
				product.Description = viewModel.Description;
			}

			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult RemoveProduct(int id)
		{
			var product = _unitOfWork.Products.GetEditingProduct(id);

			_unitOfWork.Products.Remove(product);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}
	}
}