using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public interface IUserService
	{
		UserListViewModel GetUserList(string searchTerm);
		UserListViewModel GetUserSearchResultList(string searchTerm);
		EditUserViewModel EditUser(string id);
		void AddUser(CreateUserViewModel viewModel);
		void DeleteUser(string id);
		void EditUser(EditUserViewModel viewModel);
		bool DoesUserExist(string id);
		UserContactDetailsViewModel GetUserContactDetails(string userId);
		void ChangeUserContactDetails(UserContactDetailsViewModel viewModel, string userId);
		int GetUserCount();
		int GetRecentlyUserCount();
	}
}