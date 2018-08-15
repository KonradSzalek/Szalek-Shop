using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Persistance;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class AdminController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public AdminController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Users(string query = null)
		{
			var viewModel = new UsersViewModel
			{
				SearchTerm = query,
				Users = _unitOfWork.UserRepository.GetUsersWithUserRole(),
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = _unitOfWork.UserRepository.GetQueriedUsersWithUserRole(query);
			}


			if (User.IsInRole("Admin"))
			{
				return View(viewModel);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public ActionResult Search(UsersViewModel viewModel)
		{
			return RedirectToAction("Users", "Admin", new { query = viewModel.SearchTerm });
		}

		[Authorize(Roles = "Admin")]
		public ActionResult AddUser()
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Add a new user"
			};

			return View("UserForm", viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult NewUser(UsersViewModel viewModel)
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
			};

			{
				string password = "secret";
				var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
				var manager = new UserManager<ApplicationUser, string>(store);

				var result = manager.Create(user, password);

				if (!result.Succeeded)
				{
					throw new ApplicationException("Unable to create a user.");
				}
				result = manager.AddToRole(user.Id, "User");

				if (!result.Succeeded)
				{
					throw new ApplicationException("Unable to add user to a role.");
				}
			}

			return RedirectToAction("Users", "Admin");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult EditUser(string id)
		{
			var user = _unitOfWork.UserRepository.GetEditingUser(id);

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
		[Authorize(Roles = "Admin")]
		public ActionResult UpdateUser(UsersViewModel viewModel)
		{
			var user = _unitOfWork.UserRepository.GetEditingUser(viewModel.Id);

			{
				user.UserName = viewModel.Email;
				user.Email = viewModel.Email;
				user.Name = viewModel.Name;
				user.Surname = viewModel.Surname;
			}

			_unitOfWork.Complete();

			return RedirectToAction("Users", "Admin");
		}
	}
}
