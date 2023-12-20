using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IRecepsionistRepository
    {
        void Add(Recepsionist recepsionist);
        Recepsionist GetById(int id);
        void DeleteById(int id);
        IEnumerable<Recepsionist> GetAll();
    }
    internal class RecepsionistRepository: IRecepsionistRepository
    {
        private readonly DbSet<Recepsionist> _dbSet;
        public RecepsionistRepository(DeliverySystemContext dbContext) 
        {
            _dbSet = dbContext.Set<Recepsionist>();
        }
        public void Add(Recepsionist recepsionist) 
        {
            _dbSet.Add(recepsionist);
        }
        public Recepsionist GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void DeleteById(int id) 
        {
            _dbSet.Remove(GetById(id));
        }
        public IEnumerable<Recepsionist> GetAll() 
        {
            return _dbSet.ToList();
        }
    }
}
