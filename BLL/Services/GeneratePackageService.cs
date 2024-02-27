using DAL;
using DAL.Entities;
using System.Data;

namespace BLL.Services
{
    public interface IGeneratePackageService : IBaseService<GeneratePackage, int>
    {
        Task<IEnumerable<GeneratePackage>> GetGeneratedPackages();
        Task RemoveGeneratedPackageAsync(int id);
    }
    internal class GeneratePackageService : BaseService<GeneratePackage, int> ,IGeneratePackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GeneratePackageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<GeneratePackage>> GetGeneratedPackages()
        {
            var package = await _unitOfWork.GeneratePackageRepository.GetAllAsync();
            return package.Select(g => new GeneratePackage
                {
                    Id = g.Id,
                    Name = g.Name,
                    Email = g.Email,
                    CreatedDate = g.CreatedDate,
                    SenderInformation = g.SenderInformation,
                    DestinationAddress = g.DestinationAddress,
                    DestinationZipCode = g.DestinationZipCode,
                    SentAddress = g.SentAddress,
                    SentZipCode = g.SentZipCode,
                    Weight=g.Weight
                });
        }
        public async Task RemoveGeneratedPackageAsync(int id)
        {
            var generatedPackage = await _unitOfWork.GeneratePackageRepository.GetByIdAsync(id);

            if (generatedPackage != null)
            {
                _unitOfWork.GeneratePackageRepository.DeleteAsync(generatedPackage);
                await _unitOfWork.CommitAsync();
            }
        }

    }
}
