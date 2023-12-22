using BLL.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    public class PackageController : Controller
    {
        private IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_packageService.GetAllPackages());
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var package = _packageService.GetById(id);
            return View(package);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PackageAddModel model) 
        { 
            if (ModelState.IsValid)
            {
                _packageService.Create(new Package
                {
                    Name = model.Name,
                    BarcodePackage = model.BarcodePackage,
                    SentAddress = model.SentAddress,
                    DestinationAddress = model.DestinationAddress
                });
                return RedirectToAction(nameof(Index));
            }
                return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var package = _packageService.GetById(id);
            return View(new PackageAddModel
            {
                Name = package.Name,
                BarcodePackage = package.BarcodePackage,
                SentAddress = package.SentAddress,
                DestinationAddress = package.DestinationAddress
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, PackageAddModel model)
        {
            if (ModelState.IsValid)
            {
                _packageService.Update(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var package = _packageService.GetById(id);
            return View(package);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var package = _packageService.GetById(id);
            return View(package);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _packageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
