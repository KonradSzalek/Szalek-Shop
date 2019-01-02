using System.Collections.Generic;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.DTO;

namespace szalkszop.Services
{
	public interface IPaymentMethodService
	{
		void AddPaymentMethod(PaymentMethodViewModel viewModel);
		void DeletePaymentMethod(int id);
		bool DoesPaymentMethodExist(int id);
		IEnumerable<PaymentMethodDto> GetList();
		PaymentMethodDto GetPaymentMethod(int id);
		void Edit(PaymentMethodViewModel viewModel);
		double? GetCost(int id);
	}
}