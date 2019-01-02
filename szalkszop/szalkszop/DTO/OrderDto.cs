using System;
using System.ComponentModel.DataAnnotations;
using static szalkszop.Core.Models.Order;

namespace szalkszop.DTO
{
	public class OrderDto
	{
		public string ShippingAddress { get; set; }
		public string Email { get; set; }
		public int Id { get; set; }
		public string CustomerId { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime OrderDate { get; set; }
		public DeliveryTypeDto DeliveryType { get; set; }
		public PaymentMethodDto PaymentMethod { get; set; }
		public double? TotalPrice { get; set; }
	}
}

