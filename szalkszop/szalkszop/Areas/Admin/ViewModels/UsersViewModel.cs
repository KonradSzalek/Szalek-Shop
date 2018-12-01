using Antlr.Runtime.Misc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class UsersViewModel
	{
		[StringLength(50)]
		public string SearchTerm { get; set; }

		public IEnumerable<UserDto> Users { get; set; }

		public IEnumerable<UserSearchResult> UsersSearch { get; set; }
	}
}