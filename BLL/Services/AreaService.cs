using DAL;
using DAL.Entities;
using System.Data;

namespace BLL.Services
{
    public interface IAreaService : IBaseService<Area, int>
    { 
        Task<IEnumerable<Area>> GetAreasAsync();
       
    }

    internal class AreaService : BaseService <Area, int>, IAreaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Area>> GetAreasAsync()
        {
            var areas = await _unitOfWork.AreaRepository.GetAllAsync();
            return areas.Select(x => new Area
            {
                Id = x.Id,
                Name = x.Name,
                ZipCode = x.ZipCode,
                AreaInformation = x.AreaInformation
            });
        }

    }
}
