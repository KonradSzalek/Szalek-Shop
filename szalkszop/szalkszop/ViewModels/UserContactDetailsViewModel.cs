using System.ComponentModel.DataAnnotations;

namespace szalkszop.ViewModels
{
	public class UserContactDetailsViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Surname")]
		public string Surname { get; set; }

		[Required]
		[Display(Name = "Address")]
		public string Address { get; set; }

		[Required]
		[Display(Name = "Postal code")]
		public string PostalCode { get; set; }

		[Required]
		[Display(Name = "City")]
		public string City { get; set; }
	}
}