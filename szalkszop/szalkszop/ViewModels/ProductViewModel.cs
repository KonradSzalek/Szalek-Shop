using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;
using szalkszop.Controllers;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.Repositories;

namespace szalkszop.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DateOfAdding { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int ProductCategory { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int AmountInStock { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
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
					ActionResult>> update = (c => c.EditProduct(this));
				Expression<Func<ProductsController,
					ActionResult>> create = (c => c.CreateProduct(this));

				var action = (Id != 0) ? update : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}

		public IEnumerable<ProductCategory> ProductCategories { get; set; }

		public IEnumerable<ProductDto> ProductsDto { get; set; }

		public IEnumerable<Product> Products { get; set; }

		public IEnumerable<ProductCategorySearchResultDto> ProductCategoriesSearchResultDto { get; set; }
	}
}