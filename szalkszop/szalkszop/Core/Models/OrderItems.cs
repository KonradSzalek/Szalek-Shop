using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class OrderItem
	{
		public int Id { get; set; }

		[Required]
		public int OrderId { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public double? Price { get; set; }
	}
}