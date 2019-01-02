using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;

namespace szalkszop.Areas.Admin.ViewModels
{
	public class PaymentMethodViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public double? Cost { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<PaymentMethodController,
					ActionResult>> edit = (c => c.Edit(this));
				Expression<Func<PaymentMethodController,
					ActionResult>> create = (c => c.Create(this));

				var action = (Id != 0) ? edit : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}
	}
}