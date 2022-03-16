using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public IConfiguration _configuration;
        public EmployeeController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.repository = employeeRepository;
            this._configuration = configuration;
        }
        [HttpGet("EmployeeMasterData")]
        public ActionResult EmployeeMaster()
        {
            var result = repository.GetAllData();
            return Ok(result);
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }
    }
}
