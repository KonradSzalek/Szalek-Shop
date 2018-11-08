using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		public ActionResult Index()
		{
			var viewModel = new ProductsWithSearchViewModel
			{
				ProductsDto = _productService.GetProducts(),
				ProductSearchViewModel = _productService.GetProductSearchViewModel(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(ProductsWithSearchViewModel searchModel)
		{
			var viewModel = new ProductsWithSearchViewModel
			{
				ProductsDto = _productService.GetQueriedProductSearch(searchModel.ProductSearchViewModel),
				ProductSearchViewModel = _productService.GetProductSearchViewModel(),
			};

			return View("Index", viewModel);
		}

		public ActionResult Create()
		{
			var viewModel = _productService.AddProductViewModel();
			viewModel.Heading = "Add a product";

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(ProductViewModel viewModel)
		{
			viewModel.Id = _productService.AddProduct(viewModel);

			bool? imageUploaded1, imageUploaded2, imageUploaded3;
			imageUploaded1 = imageUploaded2 = imageUploaded3 = null;

			if (viewModel.File1 != null)
			{
				viewModel.File1.SaveAs(HttpContext.Server.MapPath("~/Images/Product")
												 + viewModel.Id + "File1.jpg");
				imageUploaded1 = true;
			}

			if (viewModel.File2 != null)
			{
				viewModel.File2.SaveAs(HttpContext.Server.MapPath("~/Images/Product")
												 + viewModel.Id + "File2.jpg");
				imageUploaded2 = true;
			}

			if (viewModel.File3 != null)
			{
				viewModel.File3.SaveAs(HttpContext.Server.MapPath("~/Images/Product")
												 + viewModel.Id + "File3.jpg");
				imageUploaded3 = true;
			}

			_productService.EditProduct(viewModel, imageUploaded1, imageUploaded2, imageUploaded3);

			return RedirectToAction("Index", "Product");
		}

		public ActionResult Edit(int id)
		{
			if (!_productService.ProductExist(id))
				return HttpNotFound();

			var viewModel = _productService.EditProductViewModel(id);
			viewModel.Heading = "Edit a product";

			return View("ProductForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductViewModel viewModel)
		{
			if (!_productService.ProductExist(viewModel.Id))
				return HttpNotFound();

			bool? imageUploaded1, imageUploaded2, imageUploaded3;
			imageUploaded1 = imageUploaded2 = imageUploaded3 = null;

			if (viewModel.File1 != null)
			{
				viewModel.File1.SaveAs(HttpContext.Server.MapPath("~/Images/Product")
												 + viewModel.Id + "File1.jpg");
				imageUploaded1 = true;
			}

			if (viewModel.File2 != null)
			{
				viewModel.File2.SaveAs(HttpContext.Server.MapPath("~/Images/Product")
												 + viewModel.Id + "File2.jpg");
				imageUploaded2 = true;
			}

			if (viewModel.File3 != null)
			{
				viewModel.File3.SaveAs(HttpContext.Server.MapPath("~/Images/Product")
												 + viewModel.Id + "File3.jpg");
				imageUploaded3 = true;
			}

			_productService.EditProduct(viewModel, imageUploaded1, imageUploaded2, imageUploaded3);


			return RedirectToAction("Index", "Product");
		}

		public ActionResult DeletePhoto(int id, string photo)
		{
			bool? imageUploaded1, imageUploaded2, imageUploaded3;
			imageUploaded1 = imageUploaded2 = imageUploaded3 = null;

			switch (photo)
			{
				case "Delete1":

					if (System.IO.File.Exists(Server.MapPath("~/Images/Product") + id + "File1.jpg"))
					{
						System.IO.File.Delete(Server.MapPath("~/Images/Product") + id + "File1.jpg");
					}
					imageUploaded1 = false;
					break;

				case "Delete2":

					if (System.IO.File.Exists(Server.MapPath("~/Images/Product") + id + "File2.jpg"))
					{
						System.IO.File.Delete(Server.MapPath("~/Images/Product") + id + "File2.jpg");
					}
					imageUploaded1 = false;
					break;

				case "Delete3":

					if (System.IO.File.Exists(Server.MapPath("~/Images/Product") + id + "File3.jpg"))
					{
						System.IO.File.Delete(Server.MapPath("~/Images/Product") + id + "File3.jpg");
					}
					imageUploaded3 = false;
					break;

				default:
					break;
			}

			_productService.DeleteProductPhotos(id, imageUploaded1, imageUploaded2, imageUploaded3);

			return RedirectToAction("Index", "Product");
		}

		public ActionResult Delete(int id)
		{
			if (!_productService.ProductExist(id))
				return HttpNotFound();

			_productService.DeleteProduct(id);

			return RedirectToAction("Index", "Product");
		}
	}
}