using System.Collections.Generic;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;

namespace szalkszop.Services
{
	public class PaymentMethodService : IPaymentMethodService
	{
		private readonly IPaymentMethodRepository _paymentMethodRepository;

		public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
		{
			_paymentMethodRepository = paymentMethodRepository;
		}

		public void AddPaymentMethod(PaymentMethodViewModel viewModel)
		{
			var paymentMethod = new PaymentMethod
			{
				Name = viewModel.Name,
				Cost = viewModel.Cost,
			};

			_paymentMethodRepository.Add(paymentMethod);
			_paymentMethodRepository.SaveChanges();
		}

		public void DeletePaymentMethod(int id)
		{
			_paymentMethodRepository.Remove(id);
			_paymentMethodRepository.SaveChanges();
		}

		public bool DoesPaymentMethodExist(int id)
		{
			return _paymentMethodRepository.Exists(id);
		}

		public void Edit(PaymentMethodViewModel viewModel)
		{
			var paymentMethod = _paymentMethodRepository.Get(viewModel.Id);
			paymentMethod.Name = viewModel.Name;
			paymentMethod.Cost = viewModel.Cost;

			_paymentMethodRepository.SaveChanges();
		}

		public IEnumerable<PaymentMethodDto> GetList()
		{
			var paymentMethodList = _paymentMethodRepository.GetPaymentMethodList();
			return Mappers.PaymentMethodMapper.MapToDto(paymentMethodList);
		}

		public PaymentMethodDto GetPaymentMethod(int id)
		{
			var paymentMethod = _paymentMethodRepository.Get(id);
			return Mappers.PaymentMethodMapper.MapToDto(paymentMethod);
		}

		public double? GetCost(int id)
		{
			return _paymentMethodRepository.Get(id).Cost;
		}
	}
}