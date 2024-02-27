using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IAreaRepository : IBaseRepository<Area, int>
    { 
        Task<IEnumerable<Area>> GetAllAsync();
    }

    public class AreaRepository : BaseRepository<Area, int>, IAreaRepository
    {
        public AreaRepository(DeliverySystemContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }

}
