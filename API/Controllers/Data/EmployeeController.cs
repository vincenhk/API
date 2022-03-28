using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

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

        [HttpGet("EmployeeData/{NIK}")]
        public ActionResult EmployeeMaster(string NIK)
        {
            var result = repository.GetAllData(NIK);
            return Ok(result);
        }

        [HttpPut("EmployeeData")]
        public ActionResult EmployeeMaster(GetAllData test)
        {
            var result = repository.updateMasterData(test);
            if (result == 1)
            {
                return Ok("Update Berhasil");
            }
            else
            {
                return BadRequest("Update Gagal");
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("CountGender")]
        public ActionResult CountGender()
        {
            try
            {
                var dataCount = repository.CountGenderEmployee();
                if (dataCount.Count != 0)
                {
                    return Ok(dataCount);
                }
                else
                {
                    return NotFound("Tidak ada data");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "COUNT DATA Server Error");
            }
        }

    }

    
}
