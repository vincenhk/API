using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
        {
            private readonly EmployeeRepository repository;
            public EmployeesController(EmployeeRepository repository) : base(repository)
            {
                this.repository = repository;
            }

            [HttpGet]
            public async Task<JsonResult> GetAllProfile()
            {
                var result = await repository.GetAllProfile();
                return Json(result);
            }

            public IActionResult Index()
            {
                return View();
            }
        }
    }
}
