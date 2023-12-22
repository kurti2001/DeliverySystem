using DAL;
using Common.DTO;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IPackageService
    {
        IEnumerable<Package> GetAllPackages();
        void Create(Package package);
        void Delete(int id);
        Package GetById(int id);
        void Update(int id, PackageAddModel model);
    }
    internal class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static List<Package> _packages = new List<Package>();
  
        public PackageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Package> GetAllPackages() 
        {
            return _unitOfWork.PackageRepository.GetAll()
                .Select(x => new Package
                {
                    IdPackage = x.IdPackage,
                    Name = x.Name,
                    BarcodePackage = x.BarcodePackage,
                    SentAddress = x.SentAddress,
                    DestinationAddress = x.DestinationAddress
                }).ToList();
        }
        public void Create(Package package) 
        {
            var existsPackage = _unitOfWork.PackageRepository.GetByBarcode(package.BarcodePackage);
            if (existsPackage != null) 
            {
                throw new Exception("There is an excisting package with this bacode");
            }
            _unitOfWork.PackageRepository.Add(new DAL.Entities.Package
                {
                Name = package.Name,
                BarcodePackage = package.BarcodePackage,
                SentAddress = package.SentAddress,
                DestinationAddress = package.DestinationAddress
            });
            _unitOfWork.Commit();
        }

        public void Delete(int id) 
        {
            _unitOfWork.PackageRepository.DeleteById(id);
            _unitOfWork.Commit();
        }
  
        public Package GetById(int id) 
        {
            var package = _unitOfWork.PackageRepository.GetById(id) ?? throw new Exception("There is no Package with this ID");
            return new Package
            {
                IdPackage = package.IdPackage,
                Name = package.Name,
                BarcodePackage = package.BarcodePackage,
                SentAddress = package.SentAddress,
                DestinationAddress = package.DestinationAddress
            };
        }

        public void Update(int id, PackageAddModel model)
        {
            var package = _unitOfWork.PackageRepository.GetById(id);
            package.Name = model.Name;
            package.BarcodePackage = model.BarcodePackage;
            package.SentAddress = model.SentAddress;
            package.DestinationAddress = model.DestinationAddress;
            _unitOfWork.Commit();
        }
    }
}
