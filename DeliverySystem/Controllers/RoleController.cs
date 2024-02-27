using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;

        public RoleController(RoleManager<IdentityRole> manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var role = _manager.Roles;
            return View(role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Role role)
        {
            return RedirectToAction("Index");
        }
    }
}
