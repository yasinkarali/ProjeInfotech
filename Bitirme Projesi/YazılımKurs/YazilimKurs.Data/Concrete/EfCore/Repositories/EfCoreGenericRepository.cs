using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using YazilimKurs.Data.Abstract;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        public EfCoreGenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            //contextteki course a eriş ve veritabanına ekleme yapma komutu kullan
            await _dbContext.Set<TEntity>().AddAsync(entity);// contexte gelen entity i ekle
            await _dbContext.SaveChangesAsync(); // veritabanına yansıt
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> entities = await _dbContext.Set<TEntity>().ToListAsync();
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            TEntity entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            int result = await _dbContext.SaveChangesAsync();
            return entity;
        }




    }
}