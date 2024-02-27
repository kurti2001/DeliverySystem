using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IPostalOfficeService : IBaseService<PostalOffice, int>
    {
        Task<IEnumerable<PostalOffice>> GetPostalOffices();
        Task<List<PostalOffice>> GetByAreaId(int areaId);
    }

    internal class PostalOfficeService : BaseService<PostalOffice, int>, IPostalOfficeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAreaService _areaService;

        public PostalOfficeService(IUnitOfWork unitOfWork, IAreaService areaService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _areaService = areaService;
        }

        public async Task<List<PostalOffice>> GetByAreaId(int areaId)
        {
            var offices = await _unitOfWork.PostalOfficeRepository.GetByAreaId(areaId)
                .ToListAsync();

            return offices.Select(x => new PostalOffice
            {
                Id = x.Id,
                OfficeName = x.OfficeName,
                Location = x.Location,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                AreaId = x.AreaId
            }).ToList();
        }



        public async Task<IEnumerable<PostalOffice>> GetPostalOffices()
        {
            var offices = await _unitOfWork.PostalOfficeRepository.GetAll();
            return offices.Select(x => new PostalOffice
            {
                Id = x.Id,
                OfficeName = x.OfficeName,
                Location = x.Location,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber
            });
        }
    }
}
