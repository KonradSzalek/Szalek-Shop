using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class Order
	{
		public int Id { get; set; }

		public DateTime OrderDate { get; set; }

		[Required]
		public string CustomerId { get; set; }

		[Required]
		public int DeliveryTypeId { get; set; }

		[Required]
		public int PaymentMethodId { get; set; }

		public DeliveryType DeliveryType { get; set; }

		public PaymentMethod PaymentMethod { get; set; }

		[Required]
		public double? TotalPrice { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string Surname { get; set; }

		[Required]
		[StringLength(255)]
		public string Address { get; set; }

		[Required]
		[StringLength(100)]
		public string PostalCode { get; set; }

		[Required]
		[StringLength(100)]
		public string City { get; set; }

		public enum OrderStatus
		{
			Pending,
			[Display(Name = "Payment received")]
			PaymentReceived,
			Dispatched,
			Delivered,
			Canceled
		};

		public OrderStatus Status { get; set; }
	}
}
