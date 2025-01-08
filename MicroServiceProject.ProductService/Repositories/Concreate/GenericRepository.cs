﻿using MicroServiceProject.ProductService.Context;
using MicroServiceProject.ProductService.Models;
using MicroServiceProject.ProductService.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MicroServiceProject.ProductService.Repositories.Concreate
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProductDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ProductDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);


        public  void Delete(T entity) =>  _dbSet.Remove(entity);


        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();


        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);


        public void Update(T entity) => _dbSet.Update(entity);
        public async Task<Product> FindAsync(Expression<Func<Product, bool>> predicate) => await _context.Set<Product>().FirstOrDefaultAsync(predicate);
        

    }
}
