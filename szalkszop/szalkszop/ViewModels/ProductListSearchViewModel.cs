using System.Collections.Generic;
using szalkszop.DTO;

namespace szalkszop.ViewModels
{
	public class ProductListSearchViewModel
	{
		//CR5FIXED nie powinienes potrzebowac dodawac nowego propa do search resultow
		//Po co ci one? Dlaczego nie mozesz wykorzystac Product DTO?
		// - ProductSearchResult powstal ze wzgledu na to, zeby model zgadzal sie 1:1 z modelem zwracanym z bazy danych. 
		// - ProductDTO jest obiektem wielopoziomowym(?) czyli ma w sobie kategorie i zdjecia przez co nie mapuje sie z bazy danych
		// - Budyn powiedzial, zebym nie uzywal mapowania tylko specjalnie stworzyl konkretny viewmodel pod te wyniki
		// - Swoją drogą tez bylo to dla mnie bardzo uciazliwe, poprzez fakt ze zwracam inne modele na gecie i na poscie
		public IEnumerable<ProductSearchResultDto> ProductSearchResultList { get; set; }
		public IEnumerable<ProductDto> ProductList { get; set; }

		//CR5FIXED po co ci ten pojedynczy prop?
		// - Zalozenie jest takie, ze wypelniam wszystkie propy searcha w serwisie i przekazuje tylko 1 prop do kontrolera, zeby byl czystszy kontroler
		public ProductFiltersViewModel ProductFiltersViewModel { get; set; }
	}
}