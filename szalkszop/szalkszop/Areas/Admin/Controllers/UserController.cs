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
			if (!ModelState.IsValid)
			{
				return View("SearchResul", viewModel);
			}

			var userViewModel = _userService.GetUsersViewModelPost(viewModel.SearchTerm);

			return View("SearchResult", userViewModel);
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

			return View("EditUserForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditUserViewModel viewModel)
		{
			if (!_userService.UserExist(viewModel.Id))
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
