using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class ProductImageDto
	{
		public Guid Id { get; set; }

		public string ImageName { get; set; }

		public string ThumbNailName { get; set; }
	}
}