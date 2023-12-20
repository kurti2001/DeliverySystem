using DAL;
using Common.DTO;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
namespace BLL.Services
{
    public interface IPackageService
    {
        List<Package> GetAllPackages();
        void Create(Package package);
        void Delete(int id);
        Package GetById(int id);
    }
    internal class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static List<Package> _packages = new List<Package> ()
        {
            new Package { IdPackage=1, BarcodePackage=123456, Name = "FIRST Package", SentAddress = "St. Sent", DestinationAddress = "St. Dest" }
        };
        

        public PackageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Package> GetAllPackages() 
        {
            return _packages;
        }
        public void Create(Package package) 
        {
            var existsPackage = _unitOfWork.PackageRepository.GetById(package.IdPackage);
            if (existsPackage != null) 
            {
                throw new Exception("There is an excisting package");
            }
            _unitOfWork.PackageRepository.Add(new DAL.Entities.Package
                {
                Name = package.Name,
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
            return _packages.FirstOrDefault(x=> x.IdPackage == id) ?? throw new Exception ("There is no package with this id");
        } 
    }
}
