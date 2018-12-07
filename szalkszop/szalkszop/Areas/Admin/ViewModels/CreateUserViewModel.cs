using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace szalkszop.ViewModels
{
	public class CreateUserViewModel
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

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}