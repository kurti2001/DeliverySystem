using BLL.Services;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    public class PostalOfficeController : Controller
    {
        private IPostalOfficeService _postalOfficeService;
        public PostalOfficeController(IPostalOfficeService postalOfficeService)
        {
            _postalOfficeService = postalOfficeService;
        }

        public IActionResult Index()
        {

            return View(_postalOfficeService.GetPostalOffices());
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var postalOffice = _postalOfficeService.GetById(id);
            return View(postalOffice);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddPostalOfficeModel model) 
        { 
            if (ModelState.IsValid)
            {
                _postalOfficeService.Create(new PostalOffice
                {
                    OfficeName= model.OfficeName,
                    Location = model.Location,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var postalOffice = _postalOfficeService.GetById(id);
            return View(postalOffice);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _postalOfficeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        { 
            var postalOffice = _postalOfficeService.GetById(id);
            return View(new AddPostalOfficeModel
            {
                OfficeName=postalOffice.OfficeName,
                Location=postalOffice.Location,
                Address=postalOffice.Address,
                PhoneNumber=postalOffice.PhoneNumber
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, AddPostalOfficeModel model)
        {
            if(ModelState.IsValid)
            {
                _postalOfficeService.Update(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
