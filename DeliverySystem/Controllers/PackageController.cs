using BLL.Services;
using Common.DTO;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using DeliverySystem.Models;

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
        [HttpPost]
        public IActionResult Create(PackageAddModel model) 
        { 
            if (ModelState.IsValid)
            {
                _packageService.Create(new Package
                {
                    Name = model.Name,
                });
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var package = _packageService.GetById(id);
            return View(new PackageAddModel
            {
                Name = package.Name
            });
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
            var package = _packageService.GetById(id);
            _packageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
