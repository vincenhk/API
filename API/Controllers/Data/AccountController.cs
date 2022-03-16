using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, int>
    {
        private readonly AccountRepository repository;
        private IConfiguration _configuration;
        private readonly MyContext context;

        public AccountController(AccountRepository accountRepository, IConfiguration configuration, MyContext context) : base(accountRepository)
        {
            this.repository = accountRepository;
            this._configuration = configuration;
            this.context = context;

        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var resultInsert = repository.Register(registerVM);

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

        [HttpGet("Login")]
        public ActionResult Login(LoginVM login) 
        {

            try
            {
                var resultLogin = repository.LoginAcc(login);

                //var getUserData = repository.GetUserData(login.Email);

                if (resultLogin > 0)
                {
                    var NIK = context.Employees.Join(context.Accounts, e => e.NIK, a => a.NIK, (e, a) => new
                    {
                        Email = e.Email,
                        NIK = a.NIK
                    }).Where(e => e.Email == login.Email).SingleOrDefault();

                    var cekrole = context.role.Where(r => r.accountRole.Any(ar => ar.NIK == NIK.NIK)).ToList();

                    //var dataRole = new LoginVM()
                    //{
                    //    Email = getUserData.Email,
                    //    Roles = getUserData.Roles
                    //};

                    var claimes = new List<Claim>();

                    claimes.Add(new Claim("Email", NIK.Email));

                    foreach (var item in cekrole)
                    {
                        claimes.Add(new Claim("roles", item.Name));
                    }
                        

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claimes,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claimes.Add(new Claim("TokenScurity", idToken.ToString()));

                    return Ok(new { status = HttpStatusCode.OK, idToken, message = "Login Success!" });
                }
                else
                {
                    return BadRequest("Email tidak ada");
                }

                //if(resultLogin == 0)
                //{
                //    return NotFound("Email Tidak ditemukan");
                //}else if(resultLogin == -401)
                //{
                //    return BadRequest("Password yang anda Masukkan Salah");
                //}else if(resultLogin == -402)
                //{
                //    return BadRequest("Unknow Eror");
                //}
                //else
                //{
                //    return Ok("Login Berhasil");
                //}
            }
            catch (Exception)
            {

                return BadRequest("Eror Login");
            }
        }

        [HttpGet("Forgot")]
        public ActionResult Forgot(ForgotVM forgotVM)
        {
            try
            {
                var result = repository.ForgotPasword(forgotVM);
                if (result == 1)
                {
                    return Ok("Cek Email");
                }
                else if (result == -101)
                {
                    return NotFound("Email tidak terdaftar");
                }
                else
                {
                    return BadRequest("Gagal");
                }
            }
            catch (Exception)
            {
                return BadRequest("Eror Message");
            }
        }

        [HttpPost("chagepassword")]
        public ActionResult ChagePassword(ChangeVM changeVM)
        {
            try
            {
                var result = repository.ChagePassword(changeVM);
                if (result == -100)
                {
                    return NotFound("Email tidak ditemukan");
                } else if (result == -101)
                {
                    return BadRequest("Kode OTP Salah");
                } else if (result == -102)
                {
                    return BadRequest("Password tidak sama dengan confirm");
                } else if (result == -500)
                {
                    return BadRequest("OTP Sudah tidak berlaku");
                } else if (result == 0)
                {
                    return BadRequest("Update Password Gagal");
                } else if (result == -400)
                {
                    return BadRequest("OTP Sudah digunakan");
                }
                else
                {
                    return Ok("Berhasil Update Password");
                }
            }
            catch (Exception)
            {

                return BadRequest("Eror Update Password");
            }
        }
    }
}
