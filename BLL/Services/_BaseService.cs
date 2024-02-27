using System;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public delegate void EntityChangedEventHandler<TEntity>(TEntity entity);

    public interface IBaseService<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TKey id, TEntity entity);
        Task DeleteAsync(TKey id);

        event EntityChangedEventHandler<TEntity> EntityCreated;
        event EntityChangedEventHandler<TEntity> EntityUpdated;
        event EntityChangedEventHandler<TEntity> EntityDeleted;
    }

    internal class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<TEntity, TKey> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<TEntity, TKey>();
        }

        public event EntityChangedEventHandler<TEntity> EntityCreated;
        public event EntityChangedEventHandler<TEntity> EntityUpdated;
        public event EntityChangedEventHandler<TEntity> EntityDeleted;

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            EntityCreated?.Invoke(entity);
        }

        public async Task UpdateAsync(TKey id, TEntity entity)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            await _unitOfWork.CommitAsync();

            EntityUpdated?.Invoke(entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();

            EntityDeleted?.Invoke(entity);
        }
    }
}
