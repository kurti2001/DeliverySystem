using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity <TKey>
    {
        Task AddAsync(TEntity item);
        Task <TEntity> GetByIdAsync(TKey id);
        void DeleteAsync(TEntity item);
    }
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
                                                          where TEntity : BaseEntity<TKey>
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IQueryable<TEntity> _noTrackingDbSet;

        public BaseRepository(DeliverySystemContext context)
        {
            _dbSet = context.Set<TEntity>();
            _noTrackingDbSet = _dbSet.AsNoTracking();
        }

        public async Task AddAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
        }

        public void DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
