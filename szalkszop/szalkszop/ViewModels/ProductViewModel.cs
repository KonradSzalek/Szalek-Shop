using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Controllers;
using szalkszop.Core.Models;

namespace szalkszop.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		[Required]
		public DateTime DateOfAdding { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		public int ProductCategory { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<HomeController,
					ActionResult>> update = (c => c.UpdateProduct(this));
				Expression<Func<HomeController,
					ActionResult>> create = (c => c.NewProduct(this));

				var action =  (Id != 0) ? update : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}

		public IEnumerable<ProductCategory> ProductCategories { get; set; }

		public IEnumerable<Product> Products { get; set; }
	}
}