using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public ActionResult Index(UsersViewModel usersViewModel)
		{
			var viewModel = _userService.GetUsersViewModel(usersViewModel.SearchTerm);

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(UsersViewModel viewModel)
		{
			var userViewModel = _userService.GetUsersViewModelPost(viewModel.SearchTerm);

			return View("SearchResult", userViewModel);
		}

		public ActionResult Create()
		{
			var viewModel = new UserViewModel
			{
				Heading = "Add a new user"
			};

			return View("UserForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(UserViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("UserForm", viewModel);
			}

			_userService.AddUser(viewModel);

			return RedirectToAction("Index", "User");
		}

		public ActionResult Delete(string id)
		{
			if (!_userService.UserExist(id))
				return HttpNotFound();

			_userService.DeleteUser(id);

			return RedirectToAction("Index", "User");
		}

		public ActionResult Edit(string id)
		{
			if (!_userService.UserExist(id))
				return HttpNotFound();

			var viewModel = _userService.EditUserViewModel(id);
			viewModel.Heading = "Edit a user";

			return View("UserForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserViewModel viewModel)
		{
			if (!_userService.UserExist(viewModel.Id))
				return HttpNotFound();

			_userService.EditUser(viewModel);

			return RedirectToAction("Index", "User");
		}
	}
}
