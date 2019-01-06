using System.Collections.Generic;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IUserRepository
	{
		IEnumerable<ApplicationUser> GetList();
		ApplicationUser Get(string id);
		IEnumerable<UserSearchResultDto> SearchUserWithStoredProcedure(string searchTerm);
		void Add(ApplicationUser user, string password);
		void Delete(string id);
		bool Exists(string id);
		void SaveChanges();
		int GetUserCount();
		int GetRecentlyUserCount();
	}
}