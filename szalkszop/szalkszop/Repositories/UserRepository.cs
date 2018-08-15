using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using szalkszop.Core.Models;

namespace szalkszop.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;
		public UserManager<ApplicationUser> userManager;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
			var userStore = new UserStore<ApplicationUser>(_context);
			userManager = new UserManager<ApplicationUser>(userStore);

		}
		public List<ApplicationUser> GetUserList()
		{
			return _context.Users.ToList();
		}

		public ApplicationUser GetEditingUser(string id)
		{
			return _context.Users.Single(u => u.Id == id);
		}

		public IEnumerable<ApplicationUser> GetQueriedUsersWithUserRole(string query)
		{
			return GetUsersWithUserRole().Where(u => (u.Surname.Contains(query) || u.Email.Contains(query)));
		}

		public IEnumerable<ApplicationUser> GetUsersWithUserRole()
		{
			return _context.Users.ToList().Where((u => userManager.IsInRole(u.Id, "User")));
		}
	}
}