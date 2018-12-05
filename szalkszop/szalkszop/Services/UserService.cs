using Microsoft.AspNet.Identity;
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
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
		{
			_userRepository = userRepository;
			_userManager = userManager;
		}

		public IEnumerable<UserDto> GetUsersWithUserRole()
		{
			var users = _userRepository.GetList().ToList().Where((u => _userManager.IsInRole(u.Id, "User"))).ToList();

			return UserMapper.MapToDto(users);
		}

		public UsersViewModel GetUsersViewModel(string searchTerm)
		{
			var viewModel = new UsersViewModel();
			viewModel.Users = GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime);

			return viewModel;
		}

		public UsersViewModel GetUsersViewModelPost(string searchTerm)
		{
			var users = _userRepository.SearchUserWithStoredProcedure(searchTerm);

			var viewModel = new UsersViewModel();
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				viewModel.SearchTerm = searchTerm;
				viewModel.UsersSearch = users;
				return viewModel;
			}
			else

				viewModel.SearchTerm = null;
			viewModel.UsersSearch = _userRepository.SearchUserWithStoredProcedure(searchTerm);

			return viewModel;
		}

		public UserViewModel EditUserViewModel(string id)
		{
			var user = _userRepository.Get(id);

			var viewModel = new UserViewModel
			{
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

			_userRepository.Add(user, viewModel.NewPassword);
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

			if (viewModel.NewPassword != null && viewModel.ConfirmPassword != null)
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(viewModel.NewPassword);
			}

			_userRepository.SaveChanges();
		}

		public bool UserExist(string id)
		{
			return _userRepository.Exists(id);
		}
	}
}