using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Models;
using szalkszop.ViewModels;

namespace szalkszop.Controllers
{
	public class AdminController : Controller
	{
		public ApplicationDbContext _context;

		public AdminController()

		{
			_context = new ApplicationDbContext();
		}
		

		public ActionResult Users(string query = null)
		{
				var viewModel = new UsersViewModel
			{
				SearchTerm = query,
				Users = _context.Users.ToList()
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = _context.Users.Where(u => u.Surname.Contains(query) || u.Email.Contains(query)).ToList();
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
				return View("NewUser", viewModel);
			}

			var user = new ApplicationUser
			{
				UserName = viewModel.Email,
				Email = viewModel.Email,
				Name = viewModel.Name,
				Surname = viewModel.Surname,
			};

			string password = "secret";

			using (var db = new ApplicationDbContext())
			{
				var store = new UserStore<ApplicationUser>(db);
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
			var user = _context.Users.Single(u => u.Id == id);
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
			var user = _context.Users.Single(u => u.Id == viewModel.Id);

			user.UserName = viewModel.Email;
			user.Email = viewModel.Email;
			user.Name = viewModel.Name;
			user.Surname = viewModel.Surname;

			_context.SaveChanges();

			return RedirectToAction("Users", "Admin");
		}
	}
}
