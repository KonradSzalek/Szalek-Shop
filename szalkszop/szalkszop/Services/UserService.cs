using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		public UserManager<ApplicationUser> userManager;
		private readonly ApplicationDbContext _context;

		public UserService(IUserRepository userRepository, ApplicationDbContext context)
		{
			_context = context;
			_userRepository = userRepository;
			var userStore = new UserStore<ApplicationUser>(_context);
			userManager = new UserManager<ApplicationUser>(userStore);
		}

		public IEnumerable<UserDto> GetUsersWithUserRole()
		{
			var users = _userRepository.GetList().ToList().Where((u => userManager.IsInRole(u.Id, "User"))).ToList();

			return UserMapper.MapToDto(users);
		}

		public UsersViewModel GetUsersViewModel(string query)
		{
			var viewModel = new UsersViewModel
			{
				SearchTerm = query,
				Users = GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime),
			};

			if (!String.IsNullOrWhiteSpace(query))
			{
				viewModel.Users = GetUsersWithUserRole().Where(u => (u.Surname.Contains(query) || u.Email.Contains(query))); ;
			}

			return viewModel;
		}

		public UserViewModel EditUserViewModel(string id)
		{
			var user = _userRepository.Get(id);

			var viewModel = new UserViewModel
			{
				Heading = "Edit a user",
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email
			};

			return viewModel;
		}

		public void AddUser(UserViewModel viewModel)
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
			_userRepository.Delete(id);
			_userRepository.SaveChanges();
		}

		public void EditUser(UserViewModel viewModel)
		{
			var user = _userRepository.Get(viewModel.Id);

			user.UserName = viewModel.Email;
			user.Email = viewModel.Email;
			user.Name = viewModel.Name;
			user.Surname = viewModel.Surname;

			_userRepository.SaveChanges();
		}

		public bool UserExist(string id)
		{
			return _userRepository.Exists(id);
		}
	}
}