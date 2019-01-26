using System.Web.Http;
using szalkszop.DTO;
using szalkszop.Services;
using static szalkszop.Core.Models.ApplicationUser;

namespace szalkszop.Controllers
{
	[AuthorizeRedirectToHomePage(Roles = "Admin")]
	public class UserController : ApiController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public IHttpActionResult Search(SearchTermDto dto)
		{
			var productList = _userService.GetUserSearchResultListApi(dto.SearchTerm);

			return Ok(productList);
		}
	}
}
