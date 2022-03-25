using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class LatihanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
