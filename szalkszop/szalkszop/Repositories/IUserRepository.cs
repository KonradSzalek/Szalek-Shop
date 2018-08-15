using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public interface IUserRepository
	{
		List<ApplicationUser> GetUserList();
		ApplicationUser GetEditingUser(string id);
		IEnumerable<ApplicationUser> GetQueriedUsersWithUserRole(string query);
		IEnumerable<ApplicationUser> GetUsersWithUserRole();
	}
}