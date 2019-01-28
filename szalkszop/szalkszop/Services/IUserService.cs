using System.Collections.Generic;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IUserService
	{
		List<ApiUserDto> GetUserSearchResultList(string searchTerm);
		UserContactDetailsViewModel GetUserContactDetails(string userId);
		EditUserViewModel EditUser(string id);
		void AddUser(CreateUserViewModel viewModel);
		void DeleteUser(string id);
		void EditUser(EditUserViewModel viewModel);
		bool DoesUserExist(string id);
		void ChangeUserContactDetails(UserContactDetailsViewModel viewModel, string userId);
		int GetUserCount();
		int GetRecentlyUserCount();
	}
}