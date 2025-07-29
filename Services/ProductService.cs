﻿using Microsoft.EntityFrameworkCore;
using WebApplication2.Interfaces;
using WebApplication2.Repository;

namespace WebApplication2.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Model.Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync<Model.Product>();
        }

        public async Task<Model.Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Model.Product> CreateAsync(Model.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Model.Product input)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) 
                return false;

            product.Name = input.Name;
            product.Price = input.Price;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) 
                return false;

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
