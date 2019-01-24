using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Repositories
{
	public interface IProductRepository
	{
		Product Get(int id);
		IQueryable<Product> GetList();
		int? GetStockAmount(int id);
		void Add(Product product);
		void Delete(int id);
		void SaveChanges();
		bool Exists(int id);
		bool PhotoExists(Guid id);
		void DeletePhoto(Guid id);
		List<string> GetPhotosNames(Guid id);
		int GetProductCount();
		List<ProductSearchResultDto> SearchResultFromSqlStoredProcedure(
			string name, 
			int? priceFrom, 
			int? priceTo,
			DateTime? dateTimeTo,
			DateTime? dateTimeFrom, 
			int productCategoryId);
	}
}