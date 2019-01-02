using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;
using szalkszop.ViewModels;
using static szalkszop.Core.Models.Order;

namespace szalkszop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IProductService _productService;
		private readonly IPaymentMethodService _paymentMethodService;
		private readonly IDeliveryTypeService _deliveryTypeService;

		public OrderService(IOrderRepository orderRepository,
			IProductService productService,
			IPaymentMethodService paymentMethodService,
			IDeliveryTypeService deliveryTypeService)
		{
			_orderRepository = orderRepository;
			_deliveryTypeService = deliveryTypeService;
			_paymentMethodService = paymentMethodService;
			_productService = productService;
		}

		public void CompleteOrder(CreateOrderViewModel viewModel, string userId)
		{
			double? totalPrice = 0;
			foreach (var item in viewModel.OrderedItemList)
			{
				double? itemPrice = item.Quantity * item.Product.Price;
				totalPrice += itemPrice;
			};

			totalPrice += _paymentMethodService.GetCost(viewModel.PaymentMethod.Id);
			totalPrice += _deliveryTypeService.GetCost(viewModel.DeliveryType.Id);

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

			order.Status = OrderStatus.Pending;

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

			SendOrderConfirmation(viewModel.UserContactDetails.Email, order.Id);

			_orderRepository.SaveChanges();
		}

		public void SendOrderConfirmation(string email, int id)
		{
			MailMessage mail = new MailMessage();
			SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

			mail.From = new MailAddress("szalekshop@gmail.com");
			mail.To.Add(email);
			mail.Subject = "Order confirmation - order no";
			mail.Body = "This is for testing SMTP mail from GMAIL";

			SmtpServer.Port = 587;
			SmtpServer.Credentials = new System.Net.NetworkCredential("szalekshop@gmail.com", "sklepinternetowy1");
			SmtpServer.EnableSsl = true;

			SmtpServer.Send(mail);
		}

		public IEnumerable<OrderDto> GetUserOrderList(string userId)
		{
			var orderList = _orderRepository.GetUserOrderList(userId).OrderByDescending(d => d.OrderDate).ToList();

			var orderDtoList = Mappers.OrderMapper.MapToDto(orderList);

			return orderDtoList;
		}

		public IEnumerable<OrderDto> GetOrderList()
		{
			var orderList = _orderRepository.GetList().OrderByDescending(d => d.OrderDate).ToList();

			var orderDtoList = Mappers.OrderMapper.MapToDto(orderList);

			return orderDtoList;
		}

		public IEnumerable<OrderItemDto> GetOrderItemList(int orderId)
		{
			var orderDtoList = new List<OrderDto>();
			var ordersItemDtoList = new List<OrderItemDto>();

			var orderItems = _orderRepository.GetOrderItemList(orderId).ToList();

			foreach (var orderItem in orderItems)
			{
				var orderItemDto = new OrderItemDto
				{
					OrderId = orderItem.OrderId,
					Name = _productService.GetProduct(orderItem.ProductId).Name,
					CategoryName = _productService.GetProduct(orderItem.ProductId).ProductCategory.Name,
					Quantity = orderItem.Quantity,
					Price = orderItem.Price,
				};
				ordersItemDtoList.Add(orderItemDto);
			}
			return ordersItemDtoList;
		}

		public void UpdateStatus(int orderId, OrderStatus? status)
		{
			var order = _orderRepository.GetOrder(orderId);
			order.Status = (OrderStatus) status;

			_orderRepository.SaveChanges();
		}
	}
}