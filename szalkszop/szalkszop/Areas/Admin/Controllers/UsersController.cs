using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;
using szalkszop.ViewModels;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		public ActionResult Index(string query = null)
		{
			var viewModel = _userService.GetUsersViewModel(query);

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Search(UsersViewModel viewModel)
		{
			return RedirectToAction("Index", "Users", new { query = viewModel.SearchTerm });
		}

		public ActionResult CreateUser()
		{
			var viewModel = _userService.AddUserViewModel();

			return View("UserForm", viewModel);
		}

		[HttpPost]
		public ActionResult CreateUser(UsersViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("UserForm", viewModel);
			}

			_userService.AddUser(viewModel);

			return RedirectToAction("Index", "Users");
		}

		public ActionResult DeleteUser(string id)
		{
			if (!_userService.IsUserExist(id))
				return HttpNotFound();

			_userService.DeleteUser(id);

			return RedirectToAction("Index", "Users");
		}

		public ActionResult EditUser(string id)
		{
			if (!_userService.IsUserExist(id))
				return HttpNotFound();

			var viewModel = _userService.EditUserViewModel(id);

			return View("UserForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditUser(UsersViewModel viewModel)
		{
			if (!_userService.IsUserExist(viewModel.Id))
				return HttpNotFound();

			_userService.EditUser(viewModel);

			return RedirectToAction("Index", "Users");
		}
	}
}
