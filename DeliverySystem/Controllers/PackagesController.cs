using BLL.Services;
using Common.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    public class PackagesController : BaseController
    {
        private readonly IPackageService _packageService;
        private readonly IGeneratePackageService _generatePackageService;
        private readonly IEmailsService _emailsService;

        public PackagesController(IPackageService packageService,
                                  IGeneratePackageService generatePackageService,
                                  IEmailsService emailsService,
                                  IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _packageService = packageService;
            _generatePackageService = generatePackageService; 
            _emailsService = emailsService;
        }
        [Authorize(Roles = "Recepsionist,Transporter")]
        public async Task<IActionResult> Index()
        {
            var package = await _packageService.GetAllPackages();
            return View(package);
        }

        [HttpGet]
        [Authorize(Roles = "Recepsionist,Transporter")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var package = await _packageService.GetByIdAsync(id);
            return View(package);
        }

        [HttpGet]
        [Authorize(Roles = "Recepsionist")]
        public IActionResult Create(string name, 
                                    string senderInformation,
                                    string email,
                                    string sentAddress,
                                    string destinationAddress,
                                    int sentZipCode,
                                    int destinationZipCode)
        {
            var model = new PackageAddModel
            {
                Name = name,
                SenderInformation = senderInformation,
                Email = email,
                SentAddress = sentAddress,
                SentZipCode = sentZipCode,
                DestinationAddress = destinationAddress,
                DestinationZipCode = destinationZipCode
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> Create(PackageAddModel model)
        {
            var onOk = async () =>
            {
                var newPackage = new Package
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
                await _generatePackageService.RemoveGeneratedPackageAsync(model.Id);

                await _packageService.CreateAsync(newPackage);

                if (!string.IsNullOrEmpty(model.Email))
                {
                    var subject = "New Package Created";
                    var body = $"Dear {model.Name}, a new package with the barcode {model.BarcodePackage} has been created for you.";

                    await _emailsService.SendEmailAsync(model.Email, subject, body);
                }
                return (IActionResult)RedirectToAction("Index");
            };
            var onError = async () =>
            {
                return (IActionResult)View(model);
            };
            return await TryExecuteAsync(async () =>
            {
                if (ModelState.IsValid)
                    return await onOk();
                return await onError();
            }, onError);
        }

        [HttpGet]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> Delete(int id)
        {
            return await TryExecuteAsync(async () =>
            {
                var package = await _packageService.GetByIdAsync(id);
                return (IActionResult)View(package);
            },
            async () =>
            {
                return (IActionResult)View("NotFound404");
            });
        }

        [HttpPost]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _packageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Recepsionist,Transporter")]
        public async Task<IActionResult> Details(int id)
        {
            var package = await _packageService.GetByIdAsync(id);
            return View(package);
        }
        [HttpGet]
        [Authorize(Roles = "Recepsionist,Transporter")]
        public async Task<IActionResult> Description(int id)
        {
            var package = await _packageService.GetByIdAsync(id);
            return View(package);
        }


        [HttpGet]
        [Authorize(Roles = "Recepsionist,Transporter")]
        public async Task<IActionResult> Edit(int id)
        {
            var package = await _packageService.GetByIdAsync(id);
            return View(new PackageAddModel
            {
                Name = package.Name,
                BarcodePackage = package.BarcodePackage,
                SenderInformation = package.SenderInformation,
                Email = package.Email,
                SentAddress = package.SentAddress,
                SentZipCode = package.SentZipCode,
                DestinationAddress = package.DestinationAddress,
                DestinationZipCode = package.DestinationZipCode
            });
        }

        [HttpPost]
        [Authorize(Roles = "Recepsionist,Transporter")]
        public async Task<IActionResult> Edit(int id, Package model)
        {
            var package = await _packageService.GetByIdAsync(id);

            package.Name = model.Name;
            package.BarcodePackage = model.BarcodePackage;
            package.SenderInformation = model.SenderInformation;
            package.Email = model.Email;
            package.SentAddress = model.SentAddress;
            package.SentZipCode = model.SentZipCode;
            package.DestinationAddress = model.DestinationAddress;
            package.DestinationZipCode = model.DestinationZipCode;

            await _packageService.UpdateAsync(id, package);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Package/search")]
        public async Task<List<PackageAddModel>> FilterPackages(string q)
        {
            return await _packageService.GetPackageByBarcode(q);
        }
        [HttpGet("Package/filter")]
        public async Task<IActionResult> FilterPackageView(string? q)
        {
            List<PackageAddModel> result = new List<PackageAddModel>();
            if (q != null)
            {
                result = await FilterPackages(q);
            }
            return View(result);
        }
        [HttpGet("Package/searchzip")]
        [Authorize(Roles = "Transporter")]
        public async Task<List<PackageAddModel>> FilterPackagesZip(string z)
        {
            return await _packageService.GetPackageByZipCode(z);
        }
        [HttpGet("Package/filterzip")]
        [Authorize(Roles = "Transporter")]
        public async Task<IActionResult> FilterPackageViewZip(string? z)
        {
            List<PackageAddModel> result = new List<PackageAddModel>();
            if (z != null)
            {
                result = await FilterPackagesZip(z);
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string body)
        {
            await _emailsService.SendEmailAsync(toEmail, subject, body);
            return RedirectToAction(nameof(Index)); 
        }
        [HttpPost]
        [Authorize(Roles = "Transporter")]
        public async Task<IActionResult> CreateDelivery(int id)
        {
            try
            {
                var package = await _packageService.GetByIdAsync(id);
                package.Status = PackageStatus.Transported;
                await _packageService.UpdateAsync(id, package);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AcceptPackage(int packageId)
        {
            try
            {
                await UpdatePackageStatus(packageId, PackageStatus.Accepted);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeclinePackage(int packageId)
        {
            try
            {
                await UpdatePackageStatus(packageId, PackageStatus.Rejected);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task UpdatePackageStatus(int packageId, PackageStatus status)
        {
            var package = await _packageService.GetByIdAsync(packageId);
            if (package != null)
            {
                package.Status = status;
                await _packageService.UpdateAsync(packageId, package);
            }
        }

    }

}
