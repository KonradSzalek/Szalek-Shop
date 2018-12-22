using System;
using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;

namespace szalkszop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;

		public OrderService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public void AddPaymentMethod()
		{

		}

		public void AddDeliveryType()
		{

		}

		public void DeletePaymentMethod()
		{

		}

		public void DeleteDeliveryType()
		{

		}

		public IEnumerable<PaymentMethodDto> GetPaymentMethodList()
		{
			var paymentMethodList = _orderRepository.GetPaymentMethodList();
			return Mappers.PaymentMethodMapper.MapToDto(paymentMethodList);
		}

		public IEnumerable<DeliveryTypeDto> GetDeliveryTypeList()
		{
			var deliveryTypeList = _orderRepository.GetDeliveryTypeList();
			return Mappers.DeliveryTypeMapper.MapToDto(deliveryTypeList);
		}

		public void CompleteOrder(OrderViewModel viewModel)
		{
			var order = new Order
			{
				Name = viewModel.UserContactDetails.Name,
				Surname = viewModel.UserContactDetails.Surname,
				Email = viewModel.UserContactDetails.Email,
				Address = viewModel.UserContactDetails.Address,
				PostalCode = viewModel.UserContactDetails.PostalCode,
				City = viewModel.UserContactDetails.City,
				OrderDate = DateTime.Now,
			};

			order.SetOrderStatus = Order.OrderStatus.Pending;

			_orderRepository.Add(order);
			_orderRepository.SaveChanges();

			foreach (var item in viewModel.OrderedItemList)
			{
				var orderItem = new OrderItem
				{
					OrderId = order.Id,
					ProductId = item.Product.Id,
					Quantity = item.Quantity,
					Price = item.Product.Price,
				};

				_orderRepository.AddOrderItem(orderItem);
			}

			SendOrderConfirmation();

			_orderRepository.SaveChanges();
		}

		public void SendOrderConfirmation()
		{
			// send email to user
		}
	}
}