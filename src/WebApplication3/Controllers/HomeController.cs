using System.Linq;
using Microsoft.AspNet.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PermissionError()
        {
            return View();
        }
        public IActionResult NotExistError()
        {
            return View();
        }
        public IActionResult NotLoggedError()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}