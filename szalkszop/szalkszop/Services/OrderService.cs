using System;
using System.Collections.Generic;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;
using System.Linq;

namespace szalkszop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IProductService _productService;

		public OrderService(IOrderRepository orderRepository, IProductService productService)
		{
			_orderRepository = orderRepository;
			_productService = productService;
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

		public void CompleteOrder(OrderViewModel viewModel, string userId)
		{
			double? totalPrice = 0;
			foreach (var item in viewModel.OrderedItemList)
			{
				double? itemPrice = item.Quantity * item.Product.Price;
				totalPrice += itemPrice;
			};

			totalPrice += viewModel.PaymentMethod.Cost;
			totalPrice += viewModel.DeliveryType.Cost;

			var order = new Order
			{
				Name = viewModel.UserContactDetails.Name,
				Surname = viewModel.UserContactDetails.Surname,
				Email = viewModel.UserContactDetails.Email,
				Address = viewModel.UserContactDetails.Address,
				PostalCode = viewModel.UserContactDetails.PostalCode,
				City = viewModel.UserContactDetails.City,
				OrderDate = DateTime.Now,
				CustomerId = userId,
				PaymentMethodId = viewModel.PaymentMethod.Id,
				DeliveryTypeId = viewModel.DeliveryType.Id,
				TotalPrice = totalPrice,
			};

			order.Status = Order.OrderStatus.Pending;

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

		public IEnumerable<OrderDto> GetUserOrderList(string userId)
		{
			var orderList = _orderRepository.GetOrderList(userId).ToList();
			var orderDtoList = new List<OrderDto>();

			foreach (var order in orderList)
			{
				var orderDto = new OrderDto
				{
					OrderId = order.Id,
					Status = order.Status,
					TotalPrice = order.TotalPrice,
					ShippingAddress = order.Surname + " " + order.Name + ", "
									+ order.Address + ", " + order.PostalCode + " "
									+ order.City,
					OrderDate = order.OrderDate,
					PaymentMethodId = order.PaymentMethodId,
					DeliveryTypeId = order.DeliveryTypeId,
				};
				orderDtoList.Add(orderDto);
			}
			return orderDtoList; 
		}

		public IEnumerable<OrderItemDto> GetUserOrderItemList(int orderId)
		{
			var orderDtoList = new List<OrderDto>();
			var ordersItemDtoList = new List<OrderItemDto>();

			var orderItems = _orderRepository.GetOrderItemList(orderId).ToList();

			foreach (var orderItem in orderItems)
			{
				var orderItemDto = new OrderItemDto
				{
					Name = _productService.GetProduct(orderItem.ProductId).Name,
					CategoryName = _productService.GetProduct(orderItem.ProductId).ProductCategory.Name,
					Quantity = orderItem.Quantity,
					Price = orderItem.Price,
				};
				ordersItemDtoList.Add(orderItemDto);
			}
			return ordersItemDtoList;
		}
	}
}