﻿using System.Web.Mvc;
using szalkszop.Services;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{  
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public ActionResult Index(UserListViewModel searchResultViewModel)
		{
			return View();
		}

		public ActionResult Create()
		{
			var viewModel = new CreateUserViewModel
			{
				Heading = "Add user"
			};

			return View("CreateUserForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(CreateUserViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Add user";
				return View("CreateUserForm", viewModel);
			}

			_userService.AddUser(viewModel);

			return RedirectToAction("Index", "User");
		}

		public ActionResult Delete(string id)
		{
			if (!_userService.DoesUserExist(id))
				return HttpNotFound();

			_userService.DeleteUser(id);

			return RedirectToAction("Index", "User");
		}

		public ActionResult Edit(string id)
		{
			if (!_userService.DoesUserExist(id))
				return HttpNotFound();

			var viewModel = _userService.EditUser(id);
			viewModel.Heading = "Edit a user";

			return View("EditUserForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditUserViewModel viewModel)
		{
			if (!_userService.DoesUserExist(viewModel.Id))
				return HttpNotFound();

			if (!ModelState.IsValid)
			{
				viewModel.Heading = "Edit a user";
				return View("EditUserForm", viewModel);
			}

			_userService.EditUser(viewModel);

			return RedirectToAction("Index", "User");
		}
	}
}
