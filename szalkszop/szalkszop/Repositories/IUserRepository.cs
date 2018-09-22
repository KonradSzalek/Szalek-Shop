using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IUserRepository
	{
		IEnumerable<ApplicationUser> GetUserList();
		ApplicationUser GetUser(string id);
		void Add(ApplicationUser user);
		void DeleteUser(string id);
		bool IsUserExist(string id);
		void SaveChanges();
	}
}