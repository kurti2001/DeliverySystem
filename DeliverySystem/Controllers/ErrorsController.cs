using BLL.Singleton;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeliverySystem.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly ILoggerService _logger;
        public ErrorsController(ILoggerService loggerService)
        {
            _logger = loggerService;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            _logger.LogError(exceptionDetails.Error);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
