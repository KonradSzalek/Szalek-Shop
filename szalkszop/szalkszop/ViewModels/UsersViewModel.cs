﻿using Antlr.Runtime.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class UsersViewModel
	{
		public string Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
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
				Expression<Func<UsersController,
					ActionResult>> update = (c => c.UpdateUser(this));
				Expression<Func<UsersController,
					ActionResult>> create = (c => c.NewUser(this));

				var action = (Id != null) ? update : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}

		[StringLength(100)]
		public string SearchTerm { get; set; }

		public IEnumerable<UserDto> Users { get; set; }
	}
}