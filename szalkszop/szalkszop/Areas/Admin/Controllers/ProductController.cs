using System;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Services;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IProductCategoryService _productCategoryService;

		public ProductController(IProductService productService, IProductCategoryService productCategoryService)
		{
			_productService = productService;
			_productCategoryService = productCategoryService;
		}

		public ActionResult Index()
		{
			var viewModel = new ProductListSearchViewModel
			{
				ProductList = _productService.GetProductList(),
				ProductFiltersViewModel = _productService.GetProductSearch(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(ProductListSearchViewModel searchModel)
		{
			ModelState.Remove("ProductFiltersViewModel.ProductCategory.Id");
			if (!ModelState.IsValid)
			{
				return View("AdminSearchResult", searchModel);
			}
			//CR5FIXED ModelState.IsValid
			//CR5FIXED kompletnie nie rozumiem tego productswithsearchViewModel, dlaczego nie mogles zrobic po prostu ProductsViewModel
			// - jak wyżej
			var viewModel = new ProductListSearchViewModel
			{
				ProductSearchResultList = _productService.GetQueriedProductList(searchModel.ProductFiltersViewModel),
				ProductFiltersViewModel = _productService.GetProductSearch(),
			};

			return View("AdminSearchResult", viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = _productService.AddProduct();
			viewModel.Heading = "Add a product";

			return View("CreateProductForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(ProductViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Files = null;
				viewModel.ProductCategoryList = _productCategoryService.GetProductCategoryList();
				viewModel.Heading = "Edit a product";
				return View("CreateProductForm", viewModel);
			}

			_productService.AddProduct(viewModel);

			return RedirectToAction("Index", "Product");
		}

		public ActionResult Edit(int id)
		{
			if (!_productService.DoesProductExist(id))
				return HttpNotFound();

			var viewModel = _productService.EditProduct(id);
			viewModel.Heading = "Edit a product";

			return View("EditProductForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductViewModel viewModel)
		{
			var isPhotoCountExceeded = _productService.IsPhotoCountExceeded(viewModel.Id, viewModel.Files.Count());

			if (!_productService.DoesProductExist(viewModel.Id))
				return HttpNotFound();

			if (isPhotoCountExceeded)
			{
				ModelState.AddModelError("Files", "Maximum images per product is 5");
			}

			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Edit a product";
				viewModel.ProductCategoryList = _productCategoryService.GetProductCategoryList();
				return View("EditProductForm", viewModel);
			}

			_productService.EditProduct(viewModel);

			return RedirectToAction("Index", "Product");
		}

		public ActionResult Delete(int id)
		{
			if (!_productService.DoesProductExist(id))
				return HttpNotFound();

			_productService.DeleteProduct(id);

			return RedirectToAction("Index", "Product");
		}

		public ActionResult DeletePhoto(Guid imageId, int productId)
		{
			if (!_productService.DoesProductPhotoExist(imageId))
				return HttpNotFound();

			_productService.DeletePhoto(imageId);

			return RedirectToAction("EditPhotos", "Product", new { productId });
		}
	}
}