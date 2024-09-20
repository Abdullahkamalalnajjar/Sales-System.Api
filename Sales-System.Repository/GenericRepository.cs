﻿using Microsoft.EntityFrameworkCore;
using Sales_System.Core.Repository;
using Sales_System.Core.Specification;
using Sales_System.Repository;
using Sales_System.Repository.AppDbContext;



namespace Sales_System.Respository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return dbContext.Set<T>().AsNoTracking().AsQueryable();
        }


        #region Static Repository
        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
           
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
          
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
             
    
            return await dbContext.Set<T>().FindAsync(id);

        }

        #endregion

        #region DynamicRepository

        public async Task<IReadOnlyCollection<T>> GetAllWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }


        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> specification)
        { 
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
        }


        #endregion

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQurey(dbContext.Set<T>(), spec);
        }
    }
}
