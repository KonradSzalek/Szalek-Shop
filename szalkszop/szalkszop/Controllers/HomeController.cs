using System;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Models;
using szalkszop.ViewModels;
using System.Data.Entity;

namespace szalkszop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HomeController()

		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index()
		{
			var viewModel = new ProductViewModel
			{
				Products = _context.Products.OrderByDescending(d => d.DateOfAdding).Take(3).ToList(),
				ProductCategories = _context.ProductsCategories.ToList(),
			};
			
			return View(viewModel);
		}

		public ActionResult Products()
		{
			var viewModel = new ProductViewModel
			{
				Products = _context.Products.Include(x => x.ProductCategory).ToList(),
			};
			
			return View(viewModel);
		}

		[Authorize]
		public ActionResult NewProduct()
		{
			var viewModel = new ProductViewModel
			{
				ProductCategories = _context.ProductsCategories.ToList(),
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
			_context.Products.Add(product);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult ProductCategories()
		{
			var viewModel = new ProductViewModel
			{
				ProductCategories = _context.ProductsCategories.ToList(),
			};
	
			return View(viewModel);
		}

		[Authorize]
		public ActionResult EditProduct(int id)
		{
			var product = _context.Products.Single(u => u.Id == id);

			var viewModel = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				ProductCategories = _context.ProductsCategories.ToList(),
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
			var product = _context.Products.Single(u => u.Id == viewModel.Id);

			product.Name = viewModel.Name;
			product.ProductCategoryId = viewModel.ProductCategory;


			_context.SaveChanges();

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
			_context.ProductsCategories.Add(category);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult RemoveCategory(int id)
		{
			var category = _context.ProductsCategories.Single(u => u.Id == id);
			_context.ProductsCategories.Remove(category);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

	}
}