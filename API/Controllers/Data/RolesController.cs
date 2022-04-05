using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository repository;

        public RolesController(RoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("testJWT")]
        public ActionResult TestJWT()
        {
            var result = repository.RegistredEmployee();
            return Ok(result);
        }
        
        [HttpGet("InfoEmployeeRole")]
        public ActionResult GetInfo()
        {
            var result = repository.RegistredEmployee();
            return Ok(result);
        }
    }

}
