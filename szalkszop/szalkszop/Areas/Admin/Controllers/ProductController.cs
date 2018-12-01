﻿using System;
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
		private readonly IProductCategoryService _productCategoryService;

		public ProductController(IProductService productService, IProductCategoryService productCategoryService)
		{
			_productService = productService;
			_productCategoryService = productCategoryService;
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
				ProductSearchResult = _productService.GetQueriedProducts(searchModel.ProductSearchViewModel),
				ProductSearchViewModel = _productService.GetProductSearchViewModel(),
			};

			return View("SearchResult", viewModel);
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
			if (!ModelState.IsValid)
			{
				return View("ProductForm", viewModel);
			}

			viewModel.Id = _productService.AddProduct(viewModel);

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
			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Edit a product";
				viewModel.ProductCategoriesDto = _productCategoryService.GetProductCategoriesList();
				return View("ProductForm", viewModel);
			}
			if (!_productService.ProductExist(viewModel.Id))
				return HttpNotFound();


			_productService.EditProduct(viewModel);

			return RedirectToAction("Index", "Product");
		}

		public ActionResult DeletePhoto(Guid id, int productId)
		{
			_productService.DeletePhoto(id, productId);

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