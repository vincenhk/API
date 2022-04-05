using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Employee")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Director")]
        [HttpGet("Admin/")]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet("Unauthorized/")]
        public IActionResult Unauthorized()
        {
            return View("401");
        }

        [HttpGet("Forbidden/")]
        public IActionResult Forbidden()
        {
            return View("403");
        }


        [HttpGet("Notfound/")]
        public IActionResult Notfound()
        {
            return View("404");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
