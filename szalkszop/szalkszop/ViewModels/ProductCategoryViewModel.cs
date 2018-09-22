using Antlr.Runtime.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using szalkszop.Areas.Admin.Controllers;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductCategoryViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public int AmountOfProducts { get; set; }

		public string Heading { get; set; }

		public IEnumerable<ProductCategorySearchResultDto> ProductCategoriesSearchResultDto { get; set; }
		public IEnumerable<ProductCategory> ProductCategories { get; set; }

		public string Action
		{
			get
			{
				Expression<Func<ProductCategoryController,
					ActionResult>> update = (c => c.EditCategory(this));
				Expression<Func<ProductCategoryController,
					ActionResult>> create = (c => c.CreateCategory(this));

				var action = (Id != 0) ? update : create;
				return (action.Body as MethodCallExpression).Method.Name;
			}
		}
	}
}