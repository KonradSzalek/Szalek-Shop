using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

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
		public IEnumerable<UserDto> GetUserList()
		{
			var users = _context.Users.ToList();

			return users.Select(n => new UserDto()
			{
				Id = n.Id,
				Name = n.Name,
				Surname = n.Surname,
				Address = n.Address,
				PostalCode = n.PostalCode,
				City = n.City,
				Email = n.Email,
			});
		}

		public ApplicationUser GetEditingUser(string id)
		{
			return _context.Users.Single(u => u.Id == id);
		}

		public IEnumerable<UserDto> GetQueriedUsersWithUserRole(string query)
		{
			var users = GetUsersWithUserRole().Where(u => (u.Surname.Contains(query) || u.Email.Contains(query)));

			return users.Select(n => new UserDto()
			{
				Id = n.Id,
				Name = n.Name,
				Surname = n.Surname,
				Address = n.Address,
				PostalCode = n.PostalCode,
				City = n.City,
				Email = n.Email,
			});
		}

		public IEnumerable<UserDto> GetUsersWithUserRole()
		{
			var users = _context.Users.ToList().Where((u => userManager.IsInRole(u.Id, "User")));

			return users.Select(n => new UserDto()
			{
				Id = n.Id,
				Name = n.Name,
				Surname = n.Surname,
				Address = n.Address,
				PostalCode = n.PostalCode,
				City = n.City,
				Email = n.Email,
			});
		}

		public void AddNewUser(ApplicationUser user)
		{
			string password = "secret";
			var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
			var manager = new UserManager<ApplicationUser, string>(store);

			var result = manager.Create(user, password);

			if (!result.Succeeded)
			{
				throw new ApplicationException("Unable to create a user.");
			}
			result = manager.AddToRole(user.Id, "User");

			if (!result.Succeeded)
			{
				throw new ApplicationException("Unable to add user to a role.");
			}
		}

		public void Remove(ApplicationUser user)
		{
			_context.Users.Remove(user);
		}
	}
}