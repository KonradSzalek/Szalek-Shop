using System.Collections.Generic;
using System.Data.Entity;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Repositories
{
	public interface IProductRepository
	{
		Product Get(int id);
		DbSet<Product> GetList();
		void Add(Product product);
		void Delete(int id);
		void SaveChanges();
		bool Exists(int id);
	}
}