using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Mappers
{
	public interface IUserMapper
	{
		IEnumerable<UserDto> MapToDto(IEnumerable<ApplicationUser> users);
		UserDto MapToDto(ApplicationUser user);
	}
}