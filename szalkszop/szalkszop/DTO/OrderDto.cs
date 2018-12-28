using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static szalkszop.Core.Models.Order;

namespace szalkszop.DTO
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public string ShippingAddress { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime OrderDate { get; set; }
		public int DeliveryTypeId { get; set; }
		public int PaymentMethodId { get; set; }
		public DeliveryTypeDto DeliveryType { get; set; }
		public PaymentMethodDto PaymentMethod { get; set; }
		public double? TotalPrice { get; set; }
	}
}