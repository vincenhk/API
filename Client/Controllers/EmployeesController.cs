using API.Models;
using API.ViewModel;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        //[HttpGet]
        //public async Task<JsonResult> GetChartUniversity()
        //{
        //    var result = await repository.GetChartUniversity();
        //    return Json(result);
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetChartGender()
        //{
        //    var result = await repository.GetChartGender();
        //    return Json(result);
        //}

        [HttpGet]
        public async Task<JsonResult> GetAllProfile()
        {
            var result = await repository.GetAllProfile();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllUniversity()
        {
            var result = await repository.GetAllUniversity();
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register([FromBody] RegisterVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }
    }
}