using System.Collections.Generic;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.DTO;

namespace szalkszop.Services
{
	public interface IDeliveryTypeService
	{
		void AddDeliveryType(DeliveryTypeViewModel viewModel);
		void DeleteDeliveryType(int id);
		bool DoesDeliveryTypeExist(int id);
		IEnumerable<DeliveryTypeDto> GetList();
		DeliveryTypeDto GetDeliveryType(int id);
		void Edit(DeliveryTypeViewModel viewModel);
		double? GetCost(int id);
	}
}
