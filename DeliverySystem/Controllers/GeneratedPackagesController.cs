using BLL.Services;
using Common.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    public class GeneratedPackagesController : BaseController
    {
        private readonly IGeneratePackageService _generatePackageService;
        private readonly IEmailsService _emailsService;

        public GeneratedPackagesController(IGeneratePackageService generatePackageService, 
                                           IEmailsService emailsService,
                                           IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _generatePackageService = generatePackageService;
            _emailsService = emailsService;
        }

        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> Index()
        {
            var generatePackages = await _generatePackageService.GetGeneratedPackages();
            var sortedPackages = generatePackages.OrderBy(package => package.CreatedDate).ToList();

            return View(sortedPackages);
        }

        [HttpGet]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> GetAreaById(int id)
        {
            var generatedPackage = await _generatePackageService.GetByIdAsync(id);
            return View(generatedPackage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneratePackageModel model)
        {
            model.CreatedDate = DateTime.UtcNow;

            var onOk = async () =>
            {
                await _generatePackageService.CreateAsync(new DAL.Entities.GeneratePackage
                {

                    Name = model.Name,
                    Email = model.Email,
                    CreatedDate = model.CreatedDate,
                    SentAddress = model.SentAddress,
                    SentZipCode = model.SentZipCode,
                    DestinationAddress = model.DestinationAddress,
                    DestinationZipCode = model.DestinationZipCode,
                    SenderInformation = model.SenderInformation,
                    Weight = model.Weight
                });
                if (!string.IsNullOrEmpty(model.Email))
                {
                    var subject = "New Package Registered";
                    var body = $"Dear {model.Name}, your request for sending a package has been sent.\nYou will be noticed when the package will be created and a barcode will be sent to track it.\nThank you,\nDeliverySystem";

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
                var generatedPackage = await _generatePackageService.GetByIdAsync(id);
                return (IActionResult)View(generatedPackage);
            },
             async () =>
             {
                 return (IActionResult)View("NotFound404");
             });
        }
        [Authorize(Roles = "Recepsionist")]
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _generatePackageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> Edit(int id)
        {
            var generatedPackage = await _generatePackageService.GetByIdAsync(id);
            return View(new GeneratePackageModel
            {
                Name = generatedPackage.Name,
                SenderInformation = generatedPackage.SenderInformation,
                Email = generatedPackage.Email,
                CreatedDate = generatedPackage.CreatedDate,
                SentAddress = generatedPackage.SentAddress,
                SentZipCode = generatedPackage.SentZipCode,
                DestinationAddress = generatedPackage.DestinationAddress,
                DestinationZipCode = generatedPackage.DestinationZipCode,
                Weight = generatedPackage.Weight
            });
        }
        [HttpPost]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> Edit(int id, GeneratePackageModel model)
        {
            var package = await _generatePackageService.GetByIdAsync(id);

            package.Name = model.Name;
            package.SenderInformation = model.SenderInformation;
            package.Email = model.Email;
            package.CreatedDate = model.CreatedDate;
            package.SentAddress = model.SentAddress;
            package.SentZipCode = model.SentZipCode;
            package.DestinationAddress = model.DestinationAddress;
            package.DestinationZipCode = model.DestinationZipCode;
            package.Weight = model.Weight;
            await _generatePackageService.UpdateAsync(id, package); 
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Recepsionist")]
        public async Task<IActionResult> Details(int id)
        {
            var generatedPackage = await _generatePackageService.GetByIdAsync(id);
            return View(generatedPackage);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string toEmail, string subject, string body)
        {
            await _emailsService.SendEmailAsync(toEmail, subject, body);
            return RedirectToAction(nameof(Index));
        }
    }
}
