using BLL.Services;
using Common.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeliverySystem.Controllers
{
    public class PostalOfficesController : BaseController
    {
        private IPostalOfficeService _postalOfficeService;
        private IAreaService _areaService;

        public PostalOfficesController(IPostalOfficeService postalOfficeService, 
                                       IAreaService areaService,
                                       IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _postalOfficeService = postalOfficeService;
            _areaService = areaService;
        }

        public async Task<IActionResult> Index()
        {
            var office = await _postalOfficeService.GetPostalOffices();
            return View(office);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var postalOffice = await _postalOfficeService.GetByIdAsync(id);
            return View(postalOffice);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var areas = await _areaService.GetAreasAsync();
            var areaModels = areas.Select(area => new SelectListItem(area.Name, area.Id.ToString())).ToList();

            ViewBag.Areas = areaModels;

            return View(new AddPostalOfficeModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AddPostalOfficeModel model)
        {
            var onOk = async () =>
            {
                await _postalOfficeService.CreateAsync(new PostalOffice
                {
                    OfficeName = model.OfficeName,
                    Location = model.Location,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    AreaId = model.AreaId
                });

            var areas = await _areaService.GetAreasAsync();
            ViewBag.Areas = areas.Select(area => new SelectListItem(area.Name, area.Id.ToString())).ToList();

            return View(model);
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
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var postalOffice = await _postalOfficeService.GetByIdAsync(id);
            return View(postalOffice);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _postalOfficeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var postalOffice = await _postalOfficeService.GetByIdAsync(id);
            return View(new AddPostalOfficeModel
            {
                OfficeName = postalOffice.OfficeName,
                Location = postalOffice.Location,
                Address = postalOffice.Address,
                PhoneNumber = postalOffice.PhoneNumber,
                AreaId = postalOffice.AreaId
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddPostalOfficeModel model)
        {
            var postalOffice = await _postalOfficeService.GetByIdAsync(id);
            postalOffice.OfficeName = model.OfficeName;
            postalOffice.Location = model.Location;
            postalOffice.Address = model.Address;
            postalOffice.PhoneNumber = model.PhoneNumber;
            postalOffice.AreaId = model.AreaId;
            await _postalOfficeService.UpdateAsync(id, postalOffice);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var package = await _postalOfficeService.GetByIdAsync(id);
            return View(package);
        }

    }
}
