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
		UsersViewModel GetUsersViewModel(string query);
		UsersViewModel AddUserViewModel();
		UsersViewModel EditUserViewModel(string id);

		void AddUser(UsersViewModel viewModel);
		void DeleteUser(string id);
		void EditUser(UsersViewModel viewModel);
		bool IsUserExist(string id);



		//IEnumerable<UserDto> GetUserList();
		//UserDto GetEditingUserDto(string id);
		//ApplicationUser GetEditingUser(string id);
		//IEnumerable<UserDto> GetUsersWithUserRole();
		//IEnumerable<UserDto> GetQueriedUsersWithUserRole(string query);
	}
}