using System.Collections.Generic;
using System.Data.Entity;
using szalkszop.Core.Models;
using szalkszop.DTO;
using szalkszop.ViewModels;

namespace szalkszop.Repositories
{
	public interface IProductRepository
	{
		void Add(Product product);
		void Remove(int id);
		Product GetProduct(int id);
		DbSet<Product> GetProductList();
		void SaveChanges();
		bool DoesProductExist(int id);
	}
}