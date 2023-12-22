using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IPackageRepository
    {
        void Add(Package package);
        Package GetById(int id);
        void DeleteById(int id);
        Package GetByBarcode(int barcode);
        void DeleteByBarcode(int barcode);
        IEnumerable<Package> GetAll();
    }

    internal class PackageRepository: IPackageRepository
    {
        private readonly DbSet<Package> _dbSet;
        public PackageRepository(DeliverySystemContext dbContext)
        {

            _dbSet = dbContext.Set<Package>();
        }
        public void Add(Package package) 
        {
            _dbSet.Add(package);
        }
        public Package GetById(int id) 
        {
            return _dbSet.Find(id);
        }
        public void DeleteById(int id) 
        {
           _dbSet.Remove(GetById(id));
        }
        public Package GetByBarcode(int barcode)
        {
            return _dbSet.Find(barcode);
        }
        public void DeleteByBarcode(int barcode)
        {
            _dbSet.Remove(GetByBarcode(barcode));
        }

        public IEnumerable<Package> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
