using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;

namespace szalkszop.Repositories
{
	public class UserMapper : IUserMapper
	{
		public IEnumerable<UserDto> MapToDto(IEnumerable<ApplicationUser> users)
		{
			return users.Select(n => MapToDto(n));
		}

		public UserDto MapToDto(ApplicationUser user)
		{
			var userDto = new UserDto()
			{
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				Address = user.Address,
				PostalCode = user.PostalCode,
				City = user.City,
				Email = user.Email,
				RegistrationDateTime = user.RegistrationDateTime,
			};

			return userDto;
		}
	}
}