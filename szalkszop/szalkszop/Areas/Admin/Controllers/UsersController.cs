using System;
using System.Linq;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Persistance;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public UsersController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index(string query = null)
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Manage users",
				SearchTerm = query,
				Users = _unitOfWork.UserRepository.GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime),
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = _unitOfWork.UserRepository.GetQueriedUsersWithUserRole(query);
			}

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(UsersViewModel viewModel)
		{
			return RedirectToAction("Index", "Admin", new { query = viewModel.SearchTerm });
		}

		public ActionResult AddUser()
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Add a new user"
			};

			return View("UserForm", viewModel);
		}

		[HttpPost]
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
				RegistrationDateTime = DateTime.Now,
			};

			_unitOfWork.UserRepository.AddNewUser(user);

			return RedirectToAction("Index", "Admin");
		}

		public ActionResult RemoveUser(string id)
		{
			var user = _unitOfWork.UserRepository.GetEditingUser(id);

			_unitOfWork.UserRepository.Remove(user);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

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
		[ValidateAntiForgeryToken]
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

			return RedirectToAction("Index", "Admin");
		}
	}
}
