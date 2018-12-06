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
		UsersViewModel GetUsersViewModelPost(string searchTerm);
		EditUserViewModel EditUserViewModel(string id);
		void AddUser(CreateUserViewModel viewModel);
		void DeleteUser(string id);
		void EditUser(EditUserViewModel viewModel);
		bool UserExist(string id);
	}
}