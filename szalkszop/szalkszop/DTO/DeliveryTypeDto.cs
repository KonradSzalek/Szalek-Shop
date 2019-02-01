using System.ComponentModel.DataAnnotations;

namespace szalkszop.DTO
{
	public class DeliveryTypeDto
	{
		[Required(ErrorMessage = "Please select the delivery type.")]
		[Range(1, int.MaxValue)]
		public int Id { get; set; }

		public string Name { get; set; }

		public double? Cost { get; set; }

		public string ListDisplayName { get; set; }
	}
}