using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Mappers
{
	public class DeliveryTypeMapper
	{
		public static IEnumerable<DeliveryTypeDto> MapToDto(IEnumerable<DeliveryType> deliveryTypes)
		{
			return deliveryTypes.Select(category => MapToDto(category));
		}

		public static DeliveryTypeDto MapToDto(DeliveryType deliveryType)
		{
			var deliveryTypeDto = new DeliveryTypeDto
			{
				Id = deliveryType.Id,
				Name = deliveryType.Name,
				Cost = deliveryType.Cost,
				ListDisplayName = deliveryType.Name + " (+ " + deliveryType.Cost + " EUR)",
			};

			return deliveryTypeDto;
		}
	}
}