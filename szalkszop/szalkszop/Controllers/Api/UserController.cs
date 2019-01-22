using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Controllers.Api
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UserController : ApiController
    {
		[HttpPost]
		public IHttpActionResult Search(UserListViewModel viewModel)
		{
			return null;
		}
    }
}
