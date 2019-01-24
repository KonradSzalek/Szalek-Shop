using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class ApiUserDto
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public string RegistrationDateTime { get; set; }

		public string EditLink { get; set; }

		public string DeleteLink { get; set; }
	}
}