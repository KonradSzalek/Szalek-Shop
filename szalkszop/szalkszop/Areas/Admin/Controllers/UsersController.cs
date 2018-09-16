using System;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly UserService _userService;

		public UsersController(UserService userService)

		{
			_userService = userService;
		}

		public ActionResult Index(string query = null)
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Manage users",
				SearchTerm = query,
				Users = _userService.GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime),
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = _userService.GetQueriedUsersWithUserRole(query);
			}

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(UsersViewModel viewModel)
		{
			return RedirectToAction("Index", "Users", new { query = viewModel.SearchTerm });
		}

		public ActionResult CreateUser()
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Add a new user"
			};

			return View("UserForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateUser(UsersViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("UserForm", viewModel);
			}

			var user = new ApplicationUser
			{
				UserName = viewModel.Email,
				Email = viewModel.Email,
				Name = viewModel.Name,
				Surname = viewModel.Surname,
				RegistrationDateTime = DateTime.Now,
			};

			_userService.AddNewUser(user);

			return RedirectToAction("Index", "Users");
		}

		public ActionResult DeleteUser(string id)
		{
			var user = _userService.GetEditingUser(id);

			_userService.Remove(user);
			_userService.Complete();

			return RedirectToAction("Index", "Users");
		}

		public ActionResult EditUser(string id)
		{
			var user = _userService.GetEditingUserDto(id);

			var viewModel = new UsersViewModel
			{
				Heading = "Edit a user",
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email
			};

			return View("UserForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateUser(UsersViewModel viewModel)
		{
			var user = _userService.GetEditingUser(viewModel.Id);

			{
				user.UserName = viewModel.Email;
				user.Email = viewModel.Email;
				user.Name = viewModel.Name;
				user.Surname = viewModel.Surname;
			}

			_userService.Complete();

			return RedirectToAction("Index", "Users");
		}
	}
}
