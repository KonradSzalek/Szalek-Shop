using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Mappers
{
	public class DeliveryTypeMapper
	{
		public static IEnumerable<DeliveryTypeDto> MapToDto(IEnumerable<DeliveryType> paymentMethods)
		{
			return paymentMethods.Select(category => MapToDto(category));
		}

		public static DeliveryTypeDto MapToDto(DeliveryType deliveryType)
		{
			var paymentMethodDto = new DeliveryTypeDto
			{
				Id = deliveryType.Id,
				Name = deliveryType.Name,
				Cost = deliveryType.Cost,
			};

			return paymentMethodDto;
		}
	}
}