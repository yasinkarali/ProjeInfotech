using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace YazilimKurs.Data.Abstract
{

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    //     TEntity GetWithFilter(Expression<Func<TEntity, bool>> filter);
    //     List<TEntity> GetAllWithFilter(Expression<Func<TEntity, bool>> filter);
    //
   }

}