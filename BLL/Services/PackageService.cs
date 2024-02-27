using AutoMapper;
using Common.DTO;
using Common.Exceptions;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public interface IPackageService : IBaseService<Package, int>
    {
        Task<IEnumerable<Package>> GetAllPackages();
        Task<List<PackageAddModel>> GetPackageByBarcode(string barcode);
        Task<List<PackageAddModel>> GetPackageByZipCode(string zipCode);
        Task UpdatePackageStatusAsync(int packageId, PackageStatus status);
        event PackageStatusChangedEventHandler PackageStatusChanged;
    }

    public delegate void PackageStatusChangedEventHandler(Package updatedPackage);

    internal class PackageService : BaseService<Package, int>, IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailsService _emailsService;
        private readonly IGeneratePackageService _generatePackageService;

        public event PackageStatusChangedEventHandler PackageStatusChanged;

        public PackageService(IUnitOfWork unitOfWork,
                              IEmailsService emailsService,
                              IGeneratePackageService generatePackageService) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _emailsService = emailsService;
            _generatePackageService = generatePackageService;

            EntityCreated += OnEntityCreated;
            EntityUpdated += OnEntityUpdated;
            EntityDeleted += OnEntityDeleted;
        }

        private async void OnEntityCreated(Package package)
        {
            await SendEmailNotification(package.Email, "Package Created", $"A new package with the Barcode {package.BarcodePackage} has been created.");
        }

        private async void OnEntityUpdated(Package package)
        {
            await SendEmailNotification(package.Email, "Package Updated", $"Package with the Barcode {package.BarcodePackage} has been updated.");

            if (package.Status == PackageStatus.Transported)
            {
                await SendEmailNotification(package.Email, "Package Delivered", $"Package with the Barcode {package.BarcodePackage} has been delivered.");
            }
        }

        private async void OnEntityDeleted(Package package)
        {
            await SendEmailNotification(package.Email, "Package Deleted", $"Package with ID {package.Id} has been deleted.");
        }

        private async Task SendEmailNotification(string userEmail, string subject, string body)
        {
            try
            {
                await _emailsService.SendEmailAsync(userEmail, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email notification: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Package>> GetAllPackages()
        {
            var packages = await _unitOfWork.PackageRepository.GetAll();

            return packages.Select(x => new Package
            {
                Id = x.Id,
                Name = x.Name,
                BarcodePackage = x.BarcodePackage,
                SenderInformation = x.SenderInformation,
                Email = x.Email,
                SentAddress = x.SentAddress,
                SentZipCode = x.SentZipCode,
                DestinationAddress = x.DestinationAddress,
                DestinationZipCode = x.DestinationZipCode,
                Status = x.Status
            });
        }

        public async Task<List<PackageAddModel>> GetPackageByBarcode(string barcode)
        {
            return (await _unitOfWork.PackageRepository
                              .FindByBarcode(barcode))
                              .Select(x => new PackageAddModel
                              {
                                  Id = x.Id,
                                  Name = x.Name,
                                  BarcodePackage = x.BarcodePackage,
                                  SenderInformation = x.SenderInformation,
                                  Email = x.Email,
                                  SentAddress = x.SentAddress,
                                  SentZipCode = x.SentZipCode,
                                  DestinationAddress = x.DestinationAddress,
                                  DestinationZipCode = x.DestinationZipCode,
                                  Status = x.Status
                              })
                              .ToList();
        }

        public async Task<List<PackageAddModel>> GetPackageByZipCode(string zipCode)
        {
            return (await _unitOfWork.PackageRepository
                                      .FindByZipCode(zipCode))
                                      .Select(x => new PackageAddModel
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
                                          BarcodePackage = x.BarcodePackage,
                                          SenderInformation = x.SenderInformation,
                                          Email = x.Email,
                                          SentAddress = x.SentAddress,
                                          SentZipCode = x.SentZipCode,
                                          DestinationAddress = x.DestinationAddress,
                                          DestinationZipCode = x.DestinationZipCode,
                                          Status = x.Status
                                      })
                                      .ToList();
        }

        public async Task CreateAsync(Package model)
        {
            var existingByBarcode = await _unitOfWork.PackageRepository.ExistsByBarcode(model.BarcodePackage);
            if (existingByBarcode)
            {
                throw new DeliverySystemException("There is an existing package with this barcode");
            }

            var newPackage = new DAL.Entities.Package
            {
                Name = model.Name,
                BarcodePackage = model.BarcodePackage,
                SenderInformation = model.SenderInformation,
                Email = model.Email,
                SentAddress = model.SentAddress,
                SentZipCode = model.SentZipCode,
                DestinationAddress = model.DestinationAddress,
                DestinationZipCode = model.DestinationZipCode,
                Status = model.Status
            };

            await _unitOfWork.PackageRepository.AddAsync(newPackage);
            await _unitOfWork.CommitAsync();

            await _generatePackageService.RemoveGeneratedPackageAsync(model.Id);
        }

        public async Task UpdatePackageStatusAsync(int packageId, PackageStatus status)
        {
            var package = await _unitOfWork.PackageRepository.GetByIdAsync(packageId);
            if (package == null)
            {
                throw new DeliverySystemException("Package not found");
            }

            package.Status = status;
            await _unitOfWork.CommitAsync();
        }
    }
}
