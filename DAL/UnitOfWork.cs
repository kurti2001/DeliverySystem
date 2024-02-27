using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
namespace DAL
{
    public interface IUnitOfWork
    {
        IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
            where TEntity : BaseEntity<TKey>;

        #region Repositories
        IAreaRepository AreaRepository { get; }
        IGeneratePackageRepository GeneratePackageRepository { get; }
        IPackageRepository PackageRepository { get; }
        IPostalOfficeRepository PostalOfficeRepository { get; }
        #endregion

        void Commit();
        Task CommitAsync();
        IDbContextTransaction BeginTransaction();
    }

    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DeliverySystemContext _dbContext;
        private IAreaRepository _areaRepository;
        private IPackageRepository _packageRepository;
        private IPostalOfficeRepository _postalofficeRepository;
        private IGeneratePackageRepository _generatePackageRepository;

        public UnitOfWork(DeliverySystemContext context)
        {
            _dbContext = context;
        }

        public IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (typeof(TEntity) == typeof(Area))
            {
                return AreaRepository as IBaseRepository<TEntity, TKey>;
            }
            else if (typeof(TEntity) == typeof(Package))
            {
                return PackageRepository as IBaseRepository<TEntity, TKey>;
            } 
            else if (typeof(TEntity) == typeof(PostalOffice))
            {
                return PostalOfficeRepository as IBaseRepository<TEntity, TKey>;
            }
            else if (typeof(TEntity) == typeof(GeneratePackage))
            {
                return GeneratePackageRepository as IBaseRepository<TEntity, TKey>;
            }

            throw new ArgumentException($"Repository for type {typeof(TEntity)} not found");
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IAreaRepository AreaRepository
        {
            get
            {
                _areaRepository ??= new AreaRepository(_dbContext);
                return _areaRepository;
            }
        }

        public IPackageRepository PackageRepository
        {
            get
            {
                _packageRepository ??= new PackageRepository(_dbContext);
                return _packageRepository;
            }
        }

        public IPostalOfficeRepository PostalOfficeRepository
        {
            get
            {
                _postalofficeRepository ??= new PostalOfficeRepository(_dbContext);
                return _postalofficeRepository;
            }
        }

        public IGeneratePackageRepository GeneratePackageRepository
        {
            get
            {
                _generatePackageRepository ??= new GeneratePackageRepository(_dbContext);
                return _generatePackageRepository;
            }
        }
    }
}
