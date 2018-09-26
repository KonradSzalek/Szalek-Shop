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

        // cr4 spokojnie mogles wstawic tutaj ViewModel jako parametr
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

			return RedirectToAction("Index", "Users");
		}

		public ActionResult Delete(string id)
		{
			if (!_userService.UserExist(id))
				return HttpNotFound();

			_userService.DeleteUser(id);

			return RedirectToAction("Index", "Users");
		}

		public ActionResult Edit(string id)
		{
			if (!_userService.UserExist(id))
				return HttpNotFound();

			var viewModel = _userService.EditUserViewModel(id);

			return View("UserForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserViewModel viewModel)
		{
			if (!_userService.UserExist(viewModel.Id))
				return HttpNotFound();

			_userService.EditUser(viewModel);

			return RedirectToAction("Index", "Users");
		}
	}
}
