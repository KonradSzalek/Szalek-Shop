using System.Collections.Generic;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Mappers
{
	public class PaymentMethodMapper
	{
		public static IEnumerable<PaymentMethodDto> MapToDto(IEnumerable<PaymentMethod> paymentMethods)
		{
			return paymentMethods.Select(category => MapToDto(category));
		}

		public static PaymentMethodDto MapToDto(PaymentMethod paymentMethod)
		{
			var paymentMethodDto = new PaymentMethodDto
			{
				Id = paymentMethod.Id,
				Name = paymentMethod.Name,
				Cost = paymentMethod.Cost,
			};

			return paymentMethodDto;
		}
	}
}