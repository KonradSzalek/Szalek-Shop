using System.Web.Mvc;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Areas.Admin.Controllers
{
	//CR5FIXED ten rpzedrostek niepotrzebnu
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class AdminController : Controller
	{
		//CR5FIXED usun ten pusty controller
		// - jest potrzebny mimo ze widok na razie jest pusty
		public ActionResult Index()
		{
			return View();
		}
	}
}
