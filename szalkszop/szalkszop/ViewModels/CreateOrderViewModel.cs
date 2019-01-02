﻿using System.Collections.Generic;
using System.ComponentModel;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class CreateOrderViewModel
	{
		public UserContactDetailsViewModel UserContactDetails { get; set; }

		public List<Item> OrderedItemList { get; set; }

		[DisplayName("Payment Method")]
		public PaymentMethodDto PaymentMethod { get; set; }

		[DisplayName("Delivery Type")]
		public DeliveryTypeDto DeliveryType { get; set; }

		public IEnumerable<PaymentMethodDto> PaymentMethods { get; set; }

		public IEnumerable<DeliveryTypeDto> DeliveryTypes { get; set; }
	}
}