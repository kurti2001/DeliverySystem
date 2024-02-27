using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IPostalOfficeRepository : IBaseRepository<PostalOffice, int>
    {
        Task<IEnumerable<PostalOffice>> GetAll();
        PostalOffice GetByName(string name);
        IQueryable<PostalOffice> GetByAreaId(int areaId);

    }
    internal class PostalOfficeRepository : BaseRepository<PostalOffice, int>, IPostalOfficeRepository
    {
        public PostalOfficeRepository(DeliverySystemContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<PostalOffice>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public PostalOffice GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.OfficeName.ToLower() == name.ToLower());
        }

        public IQueryable<PostalOffice> GetByAreaId(int areaId)
        {
            return _noTrackingDbSet.Where(x => x.AreaId == areaId);
        }


    }
}
