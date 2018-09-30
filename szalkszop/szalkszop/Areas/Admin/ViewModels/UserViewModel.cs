using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;

namespace szalkszop.ViewModels
{
	public class UserViewModel
	{
		public string Id { get; set; }

		[Required]
		[StringLength(100)]
		[DisplayName("First Name")]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		[DisplayName("Last Name")]
		public string Surname { get; set; }

		[Required]
		[StringLength(100)]
		[EmailAddress]
		public string Email { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<UserController,
					ActionResult>> edit = (c => c.Edit(this));
				Expression<Func<UserController,
					ActionResult>> create = (c => c.Create(this));

				var action = (Id != null) ? edit : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}
	}
}