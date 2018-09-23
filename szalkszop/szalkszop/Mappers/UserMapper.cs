using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public static class UserMapper
	{
		public static IEnumerable<UserDto> MapToDto(IEnumerable<ApplicationUser> users)
		{
			return users.Select(n => MapToDto(n));
		}

		public static UserDto MapToDto(ApplicationUser user)
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