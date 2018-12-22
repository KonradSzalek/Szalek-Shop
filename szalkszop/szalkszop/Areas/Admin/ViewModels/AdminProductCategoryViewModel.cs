using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;

namespace szalkszop.ViewModels
{
	public class AdminProductCategoryViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
		//CR5FIXED nigdzie nie uzywane (chyba), upewnij sie i wywal
		// zgadza się, parametr wyświetlany jest tylko przy wyswietlaniu list product categories w innym viewmodelu
		// public int AmountOfProducts { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<ProductCategoryController,
					ActionResult>> edit = (c => c.Edit(this));
				Expression<Func<ProductCategoryController,
					ActionResult>> create = (c => c.Create(this));

				var action = (Id != 0) ? edit : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}
	}
}