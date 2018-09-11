﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;
using szalkszop.Controllers;
using szalkszop.DTO;
using szalkszop.Repositories;

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

		[Required]
		public int AmountInStock { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		[StringLength(255)]
		public string Description { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<ProductsController,
					ActionResult>> update = (c => c.UpdateProduct(this));
				Expression<Func<ProductsController,
					ActionResult>> create = (c => c.NewProduct(this));

				var action = (Id != 0) ? update : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}

		public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }

		public IEnumerable<ProductDto> Products { get; set; }
	}
}