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
		private readonly IUserService _userService;

		public object Status { get; private set; }

		public OrderService(IOrderRepository orderRepository,
			IProductService productService,
			IPaymentMethodService paymentMethodService,
			IDeliveryTypeService deliveryTypeService,
			IUserService userService)
		{
			_orderRepository = orderRepository;
			_deliveryTypeService = deliveryTypeService;
			_paymentMethodService = paymentMethodService;
			_productService = productService;
			_userService = userService;
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

			_orderRepository.SaveChanges();
			SendEmailWithOrderStatus(order.Id);
		}

		public void SendEmailWithOrderStatus(int orderId)
		{
			MailMessage mail = new MailMessage();
			SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

			var order = GetOrder(orderId);
			var user = _userService.GetUserContactDetails(order.CustomerId);
			var orderItemList = GetOrderItemList(order.Id).ToList();


			List<string> orderItemsTable = new List<string>();

			for (int i = 0; i < orderItemList.Count(); i++)
			{
				if (i == 0)
				{
					orderItemsTable.Add("<table><tr><th>Product Name</th><th>Category</th><th>Product Price</th><th>Quantity</th></tr>");
				}

				string productName, productPrice, productCategory, quantity;
				productName = "<td align=\"center\">" + orderItemList[i].Name + "</td>";
				productCategory = "<td align=\"center\">" + orderItemList[i].CategoryName + "</td>";
				productPrice = "<td align=\"center\">" + orderItemList[i].Price + " €</td>";
				quantity = "<td align=\"center\">" + orderItemList[i].Quantity + "</td>";
				orderItemsTable.Add("<tr>" + productName + productCategory + productPrice + quantity + "</tr>");

				if (i == (orderItemList.Count() - 1))
				{
					orderItemsTable.Add("</table>");
				}
			}

			string full = string.Empty;

			foreach (string row in orderItemsTable)
			{
				full += row;
			}

			mail.IsBodyHtml = true;
			mail.From = new MailAddress("szalekshop@gmail.com");
			mail.To.Add(order.Email);

			if (order.Status == OrderStatus.Pending)
			{
				mail.Subject = "Order confirmation - order №" + order.Id;
				mail.Body = "<html><body>Hello " + user.Name + "! We would like to confirm your following order: <br><br>" + full + "<br> Shipping addres for this order is: <br>" + order.ShippingAddress
					+ "<br><br>The parcel will be delivered by " + order.DeliveryType.Name + ". <br><br>Current status of your order is: <strong>" + order.Status +
					"</strong>. We will be informing you about any changes to your order Status.</body></html>";
			}

			else if (order.Status == OrderStatus.Canceled)
			{
				mail.Subject = "Order status change - order №" + order.Id + ": " + order.Status;
				mail.Body = "<html><body>Hello " + user.Name + "! We would like to inform you that following order: <br><br>" + full + "<br> has been canceled <strong>" + order.Status +
					"</strong>.";
			}

			else if (order.Status == OrderStatus.Delivered)
			{
				mail.Subject = "Order status change - order №" + order.Id + ": " + order.Status;
				mail.Body = "<html><body>Hello " + user.Name + "! We would like to inform you that following order: <br><br>" + full + "<br> has been delivered to following address: " 
					+ order.ShippingAddress + ".";
			}

			else
			{
				mail.Subject = "Order status change - order №" + order.Id + ": " + order.Status;
				mail.Body = "<html><body>Hello " + user.Name + "! We would like to inform you that following order: <br><br>" + full + "<br> changed status to <strong>" + order.Status +
					"</strong>.<br><br> Shipping addres for this order is: <br>" + order.ShippingAddress
					+ ".<br><br>The parcel will be delivered by " + order.DeliveryType.Name +
					". <br><br>We will be informing you about any changes to your order Status.</body></html>";
			}

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

		public OrderDto GetOrder(int orderId)
		{
			var order = _orderRepository.GetOrder(orderId);
			return Mappers.OrderMapper.MapToDto(order);
		}

		public void UpdateStatus(int orderId, OrderStatus? status)
		{
			var order = _orderRepository.GetOrder(orderId);
			order.Status = (OrderStatus) status;

			SendEmailWithOrderStatus(order.Id);

			_orderRepository.SaveChanges();
		}

		public int GetPendingOrderCount()
		{
			return _orderRepository.GetPendingOrderCount();
		}

		public int GetOrderCount()
		{
			return _orderRepository.GetOrderCount();
		}

		public bool DoesOrderExist(int id)
		{
			return _orderRepository.Exists(id);
		}
	}
}