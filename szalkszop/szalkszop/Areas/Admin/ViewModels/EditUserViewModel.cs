using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace szalkszop.ViewModels
{
	public class EditUserViewModel
	{
		public string Id { get; set; }

		[Required]
		[StringLength(100)]
		[DisplayName("First Name")]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		[DisplayName("Last Name")]
		public string Surname { get; set; }

		[Required]
		[StringLength(100)]
		[EmailAddress]
		public string Email { get; set; }

		public string Heading { get; set; }

		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Edit Password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm edited password")]
		[System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}