using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public class UserMapper
	{
		public IEnumerable<UserDto> MapToDto(List<ApplicationUser> users)
		{
			return users.Select(n => new UserDto()
			{
				Id = n.Id,
				Name = n.Name,
				Surname = n.Surname,
				Address = n.Address,
				PostalCode = n.PostalCode,
				City = n.City,
				Email = n.Email,
				RegistrationDateTime = n.RegistrationDateTime,
			});
		}
	}
}