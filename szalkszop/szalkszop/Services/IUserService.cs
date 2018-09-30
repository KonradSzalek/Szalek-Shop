using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IUserService
	{
		UsersViewModel GetUsersViewModel(string searchTerm);
		UserViewModel EditUserViewModel(string id);
		void AddUser(UserViewModel viewModel);
		void DeleteUser(string id);
		void EditUser(UserViewModel viewModel);
		bool UserExist(string id);
	}
}