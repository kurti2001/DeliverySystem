using BLL.Services;
using Common.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    public class AreasController : BaseController
    {
        private readonly IAreaService _areaService;

        public AreasController(IAreaService areaService,
                                IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _areaService = areaService;
        }
        public async Task<IActionResult> Index()
        {
            var areas = await _areaService.GetAreasAsync();
            return View(areas);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAreaById(int id)
        {
            var area = await _areaService.GetByIdAsync(id);
            return View(area);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AddAreaModel model)
        {
            var onOk = async () =>
            {
                await _areaService.CreateAsync(new Area
                {
                    Name = model.Name,
                    ZipCode = model.ZipCode,
                    AreaInformation = model.AreaInformation
                });
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return await TryExecuteAsync(async () =>
            {
                var area = await _areaService.GetByIdAsync(id);
                return (IActionResult)View(area);
            },
            async () =>
                 {
                     return (IActionResult)View("NotFound404");
                 });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _areaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var area = await _areaService.GetByIdAsync(id);
            return View(area);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var area = await _areaService.GetByIdAsync(id);
            return View(new AddAreaModel
            {
                Name = area.Name,
                ZipCode = area.ZipCode,
                AreaInformation = area.AreaInformation
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Area model)
        {
            var area = await _areaService.GetByIdAsync(id);

            area.Name = model.Name;
            area.ZipCode = model.ZipCode;
            area.AreaInformation = model.AreaInformation;

            await _areaService.UpdateAsync(id, area);
            return RedirectToAction(nameof(Index));
        }
    }
}
