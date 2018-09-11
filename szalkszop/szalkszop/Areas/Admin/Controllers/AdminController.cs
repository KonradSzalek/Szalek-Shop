using System.Web.Mvc;
using szalkszop.Core.Models;

namespace szalkszop.Areas.Admin.Controllers
{
	public class AdminController : Controller
	{
		[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
		public ActionResult Index()
		{
			return View();
		}
	}
}
