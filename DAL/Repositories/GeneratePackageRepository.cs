using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface IGeneratePackageRepository : IBaseRepository<GeneratePackage, int>
    {
        Task <IEnumerable<GeneratePackage>> GetAllAsync();
    }
    internal class GeneratePackageRepository : BaseRepository<GeneratePackage, int>, IGeneratePackageRepository
    {
        public GeneratePackageRepository(DeliverySystemContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<GeneratePackage>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

    }
}
