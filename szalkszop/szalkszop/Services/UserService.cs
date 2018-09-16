using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;

namespace szalkszop.Services
{
    // cr2 stworz interfejs pod te klase i injectuj go w kontrolerze przy pomocy interfejsu 
	public class UserService
	{
		private readonly IUserRepository _userRepository;
		private readonly UserMapper _userMapper;
		public UserManager<ApplicationUser> userManager;
		private readonly ApplicationDbContext _context;

		public UserService(IUserRepository userRepository, UserMapper userMapper, ApplicationDbContext context)
		{
			_context = context;
			_userRepository = userRepository;
			_userMapper = userMapper;
			var userStore = new UserStore<ApplicationUser>(_context);
			userManager = new UserManager<ApplicationUser>(userStore);
		}

		public IEnumerable<UserDto> GetUserList()
		{
			var users = _userRepository.GetUserList().ToList();

			return _userMapper.MapToDto(users);
		}

		public UserDto GetEditingUserDto(string id)
		{
			var user = _userRepository.GetUser(id);

			return _userMapper.MapToDto(user);
		}

		public ApplicationUser GetEditingUser(string id)
		{
			return _userRepository.GetUser(id);
		}

		public IEnumerable<UserDto> GetUsersWithUserRole()
		{
			var users = _userRepository.GetUserList().ToList().Where((u => userManager.IsInRole(u.Id, "User"))).ToList();

			return _userMapper.MapToDto(users);
		}

		public IEnumerable<UserDto> GetQueriedUsersWithUserRole(string query)
		{
			return GetUsersWithUserRole().Where(u => (u.Surname.Contains(query) || u.Email.Contains(query)));
		}

		public void AddNewUser(ApplicationUser user)
		{
			_userRepository.Add(user);
		}

		public void Remove(ApplicationUser user)
		{
			_userRepository.Remove(user);
		}

		public void Complete()
		{
			_userRepository.Complete();
		}
	}
}