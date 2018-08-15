using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class ProductCategory
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
	}
}