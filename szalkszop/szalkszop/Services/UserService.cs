using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Mappers;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserMapper _userMapper;
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
			var users = _userRepository.GetUserList();

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

		public void AddUser(UsersViewModel viewModel)
		{
			var user = new ApplicationUser
			{
				UserName = viewModel.Email,
				Email = viewModel.Email,
				Name = viewModel.Name,
				Surname = viewModel.Surname,
				RegistrationDateTime = DateTime.Now,
			};

			_userRepository.Add(user);
			_userRepository.SaveChanges();
		}

		public void DeleteUser(string id)
		{
			_userRepository.Remove(id);
			_userRepository.SaveChanges();
		}

		public void EditUser(UsersViewModel viewModel)
		{
			var user = GetEditingUser(viewModel.Id);

			{
				user.UserName = viewModel.Email;
				user.Email = viewModel.Email;
				user.Name = viewModel.Name;
				user.Surname = viewModel.Surname;
			}

			_userRepository.SaveChanges();
		}

		public bool IsUserExist(string id)
		{
			return _userRepository.IsUserExist(id);
		}

		public UsersViewModel GetUsersViewModel(string query)
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Manage users",
				SearchTerm = query,
				Users = GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime),
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = GetQueriedUsersWithUserRole(query);
			}

			return viewModel;
		}

		public UsersViewModel AddUserViewModel()
		{
			var viewModel = new UsersViewModel
			{
				Heading = "Add a new user"
			};

			return viewModel;
		}

		public UsersViewModel EditUserViewModel(string id)
		{
			var user = GetEditingUserDto(id);

			var viewModel = new UsersViewModel
			{
				Heading = "Edit a user",
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email
			};

			return viewModel;
		}
	}
}