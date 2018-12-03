using System.Web.Mvc;
using szalkszop.Core.Models;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class AdminController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
