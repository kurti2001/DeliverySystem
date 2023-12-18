using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IPostalOfficeRepository
    {
        void Add(PostalOffice postalOffice);
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
    }
}
