using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.Areas.Admin.ViewModels
{
	public class UserSearchResult
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public DateTime RegistrationDateTime { get; set; }
	}
}