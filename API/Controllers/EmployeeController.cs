using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepositiory;
        public EmployeeController(EmployeeRepository employeeRepositiory)
        {
            this.employeeRepositiory = employeeRepositiory;
        }

        //insert data
        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            try
            {
                var resultInsert = employeeRepositiory.Insert(employee);

                if (resultInsert == 0)
                {
                    return BadRequest("Data Gagal ditambahkan");
                }
                else if (resultInsert == -101)
                {
                    return BadRequest("Gagal input, Email sudah Digunakan");
                }
                else if (resultInsert == -102)
                {
                    return BadRequest("Gagal input, Phone sudah Digunakan");
                }
                else if (resultInsert == -202)
                {
                    return BadRequest("Gagal Input, Email dan Phone sudah digunakan");
                }
                else
                {
                    return Ok("Data Berhasil ditambahkan");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error inserting data");
            }
        }

        //update data
        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            try
            {
                var temp = employeeRepositiory.update(employee);
                if (temp != 0)
                {
                    return Ok($"NIK {employee.NIK} Berhasil Diupdate");
                }
                else
                {
                    return NotFound($"Data NIK {employee.NIK} tidak ditemukan ");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Data NIK {employee.NIK} tidak ditemukan ");
            }
        }

        //show data
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var temp = employeeRepositiory.Get();
                if (temp.Any())
                {
                    return Ok(temp);
                }
                else
                {
                    return NotFound("Data Employee Kosong");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error Server");
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            try
            {
                Employee temp = employeeRepositiory.Get(NIK);
                if (temp == null)
                {
                    return NotFound($"Data NIK {NIK} tidak ditemukan");
                }
                else
                {
                    return Ok(employeeRepositiory.Get(NIK));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error data");
            }
        }

        //delete data
        [HttpDelete("{NIK}")]
        public ActionResult<Employee> Delete(string NIK)
        {
            try
            {
                var deleteData = employeeRepositiory.delete(NIK);

                if (deleteData == -100)
                {
                    return NotFound($"Employee with NIK = {NIK} Not Found");
                }
                else
                {
                    return Ok("Data berhasil dihapus");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
            }
        }

        //public string AutoNIK()
        //{
        //    //*saat melakukan register, silahkan lakukan auto increment untuk NIK dengan format, Tahun+00+increment. 
        //    //Contoh: 2022001, 2022002,2022003... dst
        //    var tempCount = employeeRepositiory.Get();
        //    DateTime date = DateTime.Now;
        //    string result;
        //    result = $"{date.ToString("yyyy")}00{tempCount.Count()+1}";
        //    return result;
        //}

    }

}
