using System;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductsController : Controller
	{
		private readonly IProductCategoryRepository _productCategoryRepository;
		private readonly IProductRepository _productRepository;

		public ProductsController(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)

		{
			_productCategoryRepository = productCategoryRepository;
			_productRepository = productRepository;
		}

		public ActionResult Search()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _productCategoryRepository.GetProductCategories()
			};

			return View("ProductSearch", viewModel);
		}

		[HttpPost]
		public ActionResult Search(ProductSearchModel searchModel)
		{
			var viewModel = new ProductViewModel
			{
				Products = _productRepository.
					GetQueriedProducts(searchModel, _productRepository.GetProductsWithCategory()),
			};

			return View("Index", viewModel);
		}

		public ActionResult Index()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Products",
				Products = _productRepository.GetProductsWithCategory(),
			};

			return View(viewModel);
		}

		public ActionResult CreateProduct()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Add a product",
				ProductCategories = _productCategoryRepository.GetProductCategories(),
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

			_productRepository.Add(product);

			_productRepository.Complete();

			return RedirectToAction("Index", "Products");
		}

		public ActionResult EditProduct(int id)
		{
			var product = _productRepository.GetEditingProduct(id);

			var viewModel = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				ProductCategories = _productCategoryRepository.GetProductCategories(),
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
			var product = _productRepository.GetEditingProduct(viewModel.Id);

			{
				product.Name = viewModel.Name;
				product.ProductCategoryId = viewModel.ProductCategory;
				product.AmountInStock = viewModel.AmountInStock;
				product.Price = viewModel.Price;
				product.Description = viewModel.Description;
			}

			_productRepository.Complete();

			return RedirectToAction("Index", "Products");
		}

		public ActionResult DeleteProduct(int id)
		{
			var product = _productRepository.GetEditingProduct(id);

			_productRepository.Remove(product);
			_productRepository.Complete();

			return RedirectToAction("Index", "Products");
		}
	}
}