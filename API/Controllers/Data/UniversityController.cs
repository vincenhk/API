using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        public UniversityController(UniversityRepository repository) : base(repository)
        {

        }
    }
}
