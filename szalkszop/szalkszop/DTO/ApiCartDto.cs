using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace szalkszop.DTO
{
	public class ApiCartDto
	{
		public int ItemCount { get; set; }
		public string MakeOrderLink { get; set; }
		public double TotalPrice { get; set; }
		public List<ApiCartProductDto> Products { get; set; }
	}
}