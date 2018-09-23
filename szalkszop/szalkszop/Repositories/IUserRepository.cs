using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public interface IUserRepository
	{
		IEnumerable<ApplicationUser> GetList();
		ApplicationUser Get(string id);
		void Add(ApplicationUser user);
		void Delete(string id);
		bool Exists(string id);
		void SaveChanges();
	}
}