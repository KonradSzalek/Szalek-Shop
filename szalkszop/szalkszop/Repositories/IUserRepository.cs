using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IUserRepository
	{
		IEnumerable<UserDto> GetUserList();
		ApplicationUser GetEditingUser(string id);
		IEnumerable<UserDto> GetQueriedUsersWithUserRole(string query);
		IEnumerable<UserDto> GetUsersWithUserRole();
		void Remove(ApplicationUser user);
		void AddNewUser(ApplicationUser user);
		void Complete();
	}
}