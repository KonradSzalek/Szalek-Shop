using System.Web.Mvc;
using szalkszop.Core.Models;

namespace szalkszop.Areas.Admin.Controllers
{
	//CR% ten rpzedrostek niepotrzebnu
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class AdminController : Controller
	{
		//CR5 usun ten pusty controller
		public ActionResult Index()
		{
			return View();
		}
	}
}
