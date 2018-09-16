using System;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductsController : Controller
	{
		private readonly ProductCategoryService _productCategoryService;
		private readonly ProductService _productService;

		public ProductsController(ProductCategoryService productCategoryService, ProductService productService)

		{
			_productCategoryService = productCategoryService;
			_productService = productService;
		}

		public ActionResult Search()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _productCategoryService.GetProductCategories()
			};

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult Search(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = _productService.
					GetQueriedProducts(searchModel, _productService.GetProductsWithCategory()),
			};

			return View("Index", viewModel);
		}

		public ActionResult Index()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = _productService.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		public ActionResult CreateProduct()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Add a product",
				ProductCategories = _productCategoryService.GetProductCategories(),
			};

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateProduct(ProductViewModel viewModel)
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

			_productService.Add(product);

			_productService.Complete();

			return RedirectToAction("Index", "Products");
		}

		public ActionResult EditProduct(int id)
		{
			var product = _productService.GetEditingProductDto(id);

			var viewModel = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				ProductCategories = _productCategoryService.GetProductCategories(),
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
			var product = _productService.GetEditingProduct(viewModel.Id);

			{
				product.Name = viewModel.Name;
				product.ProductCategoryId = viewModel.ProductCategory;
				product.AmountInStock = viewModel.AmountInStock;
				product.Price = viewModel.Price;
				product.Description = viewModel.Description;
			}

			_productService.Complete();

			return RedirectToAction("Index", "Products");
		}

		public ActionResult DeleteProduct(int id)
		{
			var product = _productService.GetEditingProduct(id);

			_productService.Remove(product);
			_productService.Complete();

			return RedirectToAction("Index", "Products");
		}
	}
}