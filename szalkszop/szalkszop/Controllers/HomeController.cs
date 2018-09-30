using System.Web.Mvc;

namespace szalkszop.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}