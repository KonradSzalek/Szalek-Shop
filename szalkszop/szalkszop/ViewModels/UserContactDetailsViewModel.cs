using System.ComponentModel.DataAnnotations;

namespace szalkszop.ViewModels
{
	public class UserContactDetailsViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		[StringLength(255)]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Name")]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Surname")]
		[StringLength(100)]
		public string Surname { get; set; }

		[Required]
		[Display(Name = "Address")]
		[StringLength(255)]
		public string Address { get; set; }

		[Required]
		[Display(Name = "Postal code")]
		[StringLength(100)]
		public string PostalCode { get; set; }

		[Required]
		[Display(Name = "City")]
		[StringLength(100)]
		public string City { get; set; }
	}
}