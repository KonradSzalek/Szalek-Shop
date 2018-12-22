using System;

namespace szalkszop.Areas.Admin.ViewModels
{
	public class UserSearchResultDto
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public DateTime RegistrationDateTime { get; set; }
	}
}