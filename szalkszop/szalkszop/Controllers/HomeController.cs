using System;
using System.Linq;
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
			return View();
		}

		public ActionResult TopThreeProducts()
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.GetThreeNewestProducts(),
			};

			return View("TopThreeProductsPartial", viewModel);
		}

		public ActionResult PartialCategories()
		{
			var products = _unitOfWork.Products.GetProductsWithCategory().ToList();

			var viewModel = new ProductViewModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetCategoriesWithAmountOfProducts(products),
			};

			return View("PartialCategories", viewModel);
		}

		public ActionResult ProductSearch()
		{
			var viewModel = new ProductSearchModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories()
			};

			return View("ProductSearch", viewModel);
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

		[Authorize(Roles = "Admin")]
		public ActionResult NewProduct()
		{
			var viewModel = new ProductViewModel
			{
				Heading = "Add a product",
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
			};

			return View("ProductForm", viewModel);
		}

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
		public ActionResult RemoveProduct(int id)
		{
			var product = _unitOfWork.Products.GetEditingProduct(id);

			_unitOfWork.Products.Remove(product);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult NewCategory()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Heading = "Add a new category",
			};
			return View("CategoryForm", viewModel);
		}

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
		public ActionResult RemoveCategory(int id)
		{
			var category = _unitOfWork.ProductCategories.GetEditingProductCategory(id);

			_unitOfWork.ProductCategories.Remove(category);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult EditCategory(int id)
		{
			var category = _unitOfWork.ProductCategories.GetEditingProductCategory(id);

			var viewModel = new ProductCategoryViewModel
			{
				Name = category.Name,
			};

			return View("CategoryForm", viewModel);
		}

		[Authorize(Roles = "Admin")]
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

		public ActionResult ShowCategories()
		{
			var viewModel = new ProductCategoryViewModel
			{
				ProductCategories = _unitOfWork.ProductCategories.GetProductCategories(),
			};

			return View("LeftPanel", viewModel);
		}


		public ActionResult ShowProductInCategory(int id)
		{
			var viewModel = new ProductViewModel
			{
				Products = _unitOfWork.Products.GetProductInCategory(id)
			};

			return View("Products", viewModel);
		}
	}
}