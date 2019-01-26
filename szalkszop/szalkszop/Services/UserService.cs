using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

		public UserListViewModel GetUserList(string searchTerm)
		{
			var viewModel = new UserListViewModel();
			viewModel.UserList = GetUsersWithUserRole().OrderByDescending(d => d.RegistrationDateTime);

			return viewModel;
		}

		public UserListViewModel GetUserSearchResultList(string searchTerm)
		{
			var users = _userRepository.SearchUserWithStoredProcedure(searchTerm);

			var viewModel = new UserListViewModel();
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				viewModel.SearchTerm = searchTerm;
				viewModel.UserSearchResultList = users.ToList();
				return viewModel;
			}
			else

			viewModel.SearchTerm = null;
			viewModel.UserSearchResultList = _userRepository.SearchUserWithStoredProcedure(searchTerm);

			return viewModel;
		}

		public List<ApiUserDto> GetUserSearchResultListApi(string searchTerm)
		{
			var requestContext = HttpContext.Current.Request.RequestContext;

			var users = _userRepository.SearchUserWithStoredProcedure(searchTerm);

			List<ApiUserDto> apiUsers = new List<ApiUserDto>();

			apiUsers = users.Select(n => new ApiUserDto()
			{
				Id = n.Id,
				Name = n.Name,
				Surname = n.Surname,
				Email = n.Email,
				RegistrationDateTime = n.RegistrationDateTime.ToString("dd/MM/yyyy"),
				EditLink = new UrlHelper(requestContext).Action("Edit", "User", new { id = n.Id, Area = "Admin" } ),
				DeleteLink = new UrlHelper(requestContext).Action("Delete", "User", new { id = n.Id, Area = "Admin" }),
			}).ToList();

			return apiUsers;
		}

		public EditUserViewModel EditUser(string id)
		{
			var user = _userRepository.Get(id);

			var viewModel = new EditUserViewModel
			{
				Id = user.Id,
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email
			};

			return viewModel;
		}

		public void AddUser(CreateUserViewModel viewModel)
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

		public void EditUser(EditUserViewModel viewModel)
		{
			var user = _userRepository.Get(viewModel.Id);

			user.UserName = viewModel.Email;
			user.Email = viewModel.Email;
			user.Name = viewModel.Name;
			user.Surname = viewModel.Surname;


			if (!string.IsNullOrWhiteSpace(viewModel.NewPassword) && !string.IsNullOrWhiteSpace(viewModel.ConfirmPassword))
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(viewModel.NewPassword);
			}

			_userRepository.SaveChanges();
		}

		public bool DoesUserExist(string id)
		{
			return _userRepository.Exists(id);
		}

		public UserContactDetailsViewModel GetUserContactDetails(string userId)
		{
			var user = _userRepository.Get(userId);

			var userContactDetails = new UserContactDetailsViewModel
			{
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email,
				Address = user.Address,
				PostalCode = user.PostalCode,
				City = user.City,
			};

			return userContactDetails;
		}

		public void ChangeUserContactDetails(UserContactDetailsViewModel viewModel, string userId)
		{
			var user = _userRepository.Get(userId);

			user.Name = viewModel.Name;
			user.Surname = viewModel.Surname;
			user.Address = viewModel.Address;
			user.PostalCode = viewModel.PostalCode;
			user.City = viewModel.City;

			_userRepository.SaveChanges();
		}

		public int GetUserCount()
		{
			return _userRepository.GetUserCount();
		}

		public int GetRecentlyUserCount()
		{
			return _userRepository.GetRecentlyUserCount();
		}
	}
}