using System.Collections.Generic;
using szalkszop.Core.Models;

namespace szalkszop.ViewModels
{
	public class CartViewModel
	{
		public string UserId { get; set; }
		public List<Item> ItemList { get; set; }
	}
}