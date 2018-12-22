using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class UserListViewModel
	{
		[StringLength(50)]
		public string SearchTerm { get; set; }

		public IEnumerable<UserDto> UserList { get; set; }

		public IEnumerable<UserSearchResultDto> UserSearchResultList { get; set; }
	}
}