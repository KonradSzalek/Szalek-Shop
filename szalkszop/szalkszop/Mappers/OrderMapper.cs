using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using static szalkszop.Core.Models.Order;

namespace szalkszop.Mappers
{
	public static class OrderMapper
	{
		public static IEnumerable<OrderDto> MapToDto(IEnumerable<Order> orders)
		{
			return orders.Select(n => MapToDto(n));
		}

		public static OrderDto MapToDto(Order order)
		{
			var orderDto = new OrderDto()
			{
				Id = order.Id,
				ShippingAddress = order.Surname + " " + order.Name + ", "
									+ order.Address + ", " + order.PostalCode + " "
									+ order.City,
				Email = order.Email,
				CustomerId = order.CustomerId,
				Status = order.Status,
				OrderDate = order.OrderDate,
				TotalPrice = order.TotalPrice,
				PaymentMethod = new PaymentMethodDto
				{
					Id = order.PaymentMethod.Id,
					Name = order.PaymentMethod.Name,
					Cost = order.PaymentMethod.Cost,
				},
				DeliveryType = new DeliveryTypeDto
				{
					Id = order.DeliveryType.Id,
					Name = order.DeliveryType.Name,
					Cost = order.DeliveryType.Cost,
				}
			};

			return orderDto;
		}
	}
}