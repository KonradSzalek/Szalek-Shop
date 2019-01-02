using System.Collections.Generic;
using szalkszop.Areas.Admin.ViewModels;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;

namespace szalkszop.Services
{
	public class DeliveryTypeService : IDeliveryTypeService
	{
		private readonly IDeliveryTypeRepository _deliveryTypeRepository;

		public DeliveryTypeService(IDeliveryTypeRepository deliveryTypeRepository)
		{
			_deliveryTypeRepository = deliveryTypeRepository;
		}

		public void AddDeliveryType(DeliveryTypeViewModel viewModel)
		{
			var deliveryType = new DeliveryType
			{
				Name = viewModel.Name,
				Cost = viewModel.Cost,
			};

			_deliveryTypeRepository.Add(deliveryType);
			_deliveryTypeRepository.SaveChanges();
		}

		public void DeleteDeliveryType(int id)
		{
			_deliveryTypeRepository.Remove(id);
			_deliveryTypeRepository.SaveChanges();
		}

		public bool DoesDeliveryTypeExist(int id)
		{
			return _deliveryTypeRepository.Exists(id);
		}

		public void Edit(DeliveryTypeViewModel viewModel)
		{
			var deliveryType = _deliveryTypeRepository.Get(viewModel.Id);
			deliveryType.Name = viewModel.Name;
			deliveryType.Cost = viewModel.Cost;

			_deliveryTypeRepository.SaveChanges();
		}

		public IEnumerable<DeliveryTypeDto> GetList()
		{
			var deliveryTypeList = _deliveryTypeRepository.GetList();
			return Mappers.DeliveryTypeMapper.MapToDto(deliveryTypeList);
		}

		public DeliveryTypeDto GetDeliveryType(int Id)
		{
			var deliveryType = _deliveryTypeRepository.Get(Id);
			return Mappers.DeliveryTypeMapper.MapToDto(deliveryType);
		}

		public double? GetCost(int Id)
		{
			return _deliveryTypeRepository.Get(Id).Cost;
		}
	}
}