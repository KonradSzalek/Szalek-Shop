﻿using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using szalkszop.Core.Models;
using szalkszop.DTO;

namespace szalkszop.Repositories
{
	public class ProductCategoryRepository : IProductCategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductCategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ProductCategory> GetProductCategoryList()
		{
			return _context.ProductsCategories;
		}

		public ProductCategory GetProductCategory(int id)
		{
			return _context.ProductsCategories.Single(u => u.Id == id);
		}

		public void Add(ProductCategory category)
		{
			_context.ProductsCategories.Add((category));
		}

		public void Remove(int id)
		{	
			_context.ProductsCategories.Remove(_context.ProductsCategories.Single(p => p.Id == id));
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public bool IsCategoryExist(int id)
		{
			return _context.ProductsCategories.Any(p => p.Id == id);
		}
	}
}