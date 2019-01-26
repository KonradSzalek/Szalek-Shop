using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class ApiProductUserDto
	{
		public string Name { get; set; }

		public int AmountInStock { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public string Thumbnail { get; set; }

		public string BuyLink { get; set; }

		public string DetailsLink { get; set; }
	}
}