using System.IO;
using System.Web;
using System.Web.Mvc;
using szalkszop.Core.Models;
using szalkszop.Services;

namespace szalkszop.Areas.Admin.Controllers
{
	[ApplicationUser.AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IProductService _productService;

		public AdminController(IProductService productService)
		{
			_productService = productService;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult FileUpload(HttpPostedFileBase file)
		{
			if (file != null)
			{
				file.SaveAs(HttpContext.Server.MapPath("~/Images/")
												 + file.FileName);

				//_productService.AddImagePath();

				string imagePath = "~/Images/" + file.FileName;
			}	
			return RedirectToAction("Index", "User");
		}
	}
}
