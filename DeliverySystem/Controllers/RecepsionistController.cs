using Microsoft.AspNetCore.Mvc;
using Common.DTO;
using BLL.Services;

namespace DeliverySystem.Controllers
{
    public class RecepsionistController : Controller
    {
        private IRecepsionistService _recepsionistService;
        public RecepsionistController(IRecepsionistService recepsionistService)
        {
            _recepsionistService = recepsionistService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_recepsionistService.GetRecepsionists());
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var recepsionist = _recepsionistService.GetById(id);
            return View(recepsionist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Recepsionist model)
        {
            if(ModelState.IsValid)
            {
                _recepsionistService.Create(new Recepsionist
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recepsionist = _recepsionistService.GetById(id);
            return View(recepsionist);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _recepsionistService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recepsionist = _recepsionistService.GetById(id);
            return View(new RecepsionistAddModel
            {
                Name = recepsionist.Name,
                PhoneNumber = recepsionist.PhoneNumber,
                Email = recepsionist.Email
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, RecepsionistAddModel model)
        {
            if(ModelState.IsValid)
            {
                _recepsionistService.Update(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var recepsionist = _recepsionistService.GetById(id);
            return View(recepsionist);
        }
    }
}
