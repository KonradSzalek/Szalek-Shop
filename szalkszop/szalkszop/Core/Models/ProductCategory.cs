using System.ComponentModel.DataAnnotations;

namespace szalkszop.Core.Models
{
	public class ProductCategory
	{
		public int Id { get; set; }

        // cr1: dlaczego na modelu sa atrybuty od walidacji? Atrybuty od walidacji maja byc jedyynie na viewmodelu, sprawdz i popraw wszystkie encje modelowe
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

        // cr1: wywal to pole stad! i zrob migracje zeby usunac te kolumne
		[Range(0, int.MaxValue)]
		public int AmountOfProducts { get; set; }
	}
}