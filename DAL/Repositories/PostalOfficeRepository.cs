using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IPostalOfficeRepository
    {
        void Add(PostalOffice postalOffice);
        PostalOffice GetById(int id);
        PostalOffice GetByName(string name);
        void DeleteById(int id);
        IEnumerable<PostalOffice> GetAll();
    }
    internal class PostalOfficeRepository : IPostalOfficeRepository
    {
        private readonly DbSet<PostalOffice> _dbSet;
        public PostalOfficeRepository(DeliverySystemContext dbContext)
        {
            _dbSet = dbContext.Set<PostalOffice>();
        }
        public void Add(PostalOffice postalOffice) 
        {
            _dbSet.Add(postalOffice);
        }
        public PostalOffice GetById(int id) 
        {
            return _dbSet.Find(id);
        }
        public PostalOffice GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.OfficeName.ToLower() == name.ToLower());
        }
        public void DeleteById(int id) 
        {
            _dbSet.Remove(GetById(id));
        }
        public IEnumerable<PostalOffice> GetAll() 
        {
            return _dbSet.ToList();
        }
    }
}
