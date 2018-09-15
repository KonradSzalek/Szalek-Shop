using System;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly IUserRepository _usersRepository;

		public UsersController(IUserRepository usersRepository)

		{
			_usersRepository = usersRepository;
		}

		public ActionResult Index(string query = null)
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Manage users",
				SearchTerm = query,
				Users = _usersRepository.GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime),
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = _usersRepository.GetQueriedUsersWithUserRole(query);
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

			_usersRepository.AddNewUser(user);

			return RedirectToAction("Index", "Users");
		}

		public ActionResult DeleteUser(string id)
		{
			var user = _usersRepository.GetEditingUser(id);

			_usersRepository.Remove(user);
			_usersRepository.Complete();

			return RedirectToAction("Index", "Users");
		}

		public ActionResult EditUser(string id)
		{
			var user = _usersRepository.GetEditingUser(id);

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
			var user = _usersRepository.GetEditingUser(viewModel.Id);

			{
				user.UserName = viewModel.Email;
				user.Email = viewModel.Email;
				user.Name = viewModel.Name;
				user.Surname = viewModel.Surname;
			}

			_usersRepository.Complete();

			return RedirectToAction("Index", "Users");
		}
	}
}
