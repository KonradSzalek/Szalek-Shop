using System;

namespace szalkszop.DTO
{
	public class UserDto
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Address { get; set; }

		public string Email { get; set; }

		public string PostalCode { get; set; }

		public string City { get; set; }

		public DateTime RegistrationDateTime { get; set; }
	}
}