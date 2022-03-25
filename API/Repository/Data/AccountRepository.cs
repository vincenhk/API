using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
        private readonly MyContext context;
        private IConfiguration _configuration;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(string password, string correctHas)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHas);
        }
        public int Register(RegisterVM registerVM)
        {
            DateTime date = DateTime.Now;
            var autoNIK = $"{date.ToString("yyyy")}00{Get().Count()}";
            int convNIK = Convert.ToInt32(autoNIK);

            Employee emp = new Employee()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = registerVM.Gender
            };
            emp.NIK = $"{convNIK + 1}";
            Account acc = new Account()
            {
                NIK = emp.NIK,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
            };
            Education edu = new Education()
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                University_Id = registerVM.University_Id
            };
            context.Educations.Add(edu);
            var result = context.SaveChanges();

            Profiling prfl = new Profiling()
            {
                NIK = emp.NIK,
                Education_id = edu.Id
            };

            AccountRole accRole = new AccountRole()
            {
                NIK = emp.NIK,
                RoleId = 3
            };


            var checkEmail = context.Employees.Where(e => e.Email == emp.Email).SingleOrDefault();
            var checkPhone = context.Employees.Where(e => e.Phone == emp.Phone).SingleOrDefault();

            if (checkEmail != null && checkPhone != null)
            {
                return -202;
            }
            else if (checkEmail != null)
            {
                return -101;
            }
            else if (checkPhone != null)
            {
                return -102;
            }

            context.Employees.Add(emp);
            context.Accounts.Add(acc);
            context.Profilings.Add(prfl);
            context.accountRole.Add(accRole);
            result = context.SaveChanges();

            return result;
        }

        public int LoginAcc(LoginVM inputLogin)
        {
            //ambil data berdasarkan email
            var checkEmail = context.Employees.Where(e => e.Email == inputLogin.Email).FirstOrDefault();

            //cek ke database apakah data email ada
            if (checkEmail != null)
            {
                //ambil data join table employee dan account
                var data = context.Employees.Join(context.Accounts,
                    e => e.NIK,
                    a => a.NIK,
                    (e, a) => new
                    {
                        NIK = e.NIK,
                        Email = e.Email,
                        Password = a.Password
                    }).SingleOrDefault(e => e.Email == inputLogin.Email);

                //ambil 1 data dari hasil join pilih email yang di input
                //var inpPass = HashPassword(inputLogin.Password);
                var decrypPass = ValidatePassword(inputLogin.Password, data.Password);

                //cek apakah email cocok dengan pasword
                if (data.Email == inputLogin.Email && decrypPass == true )
                {
                    return 1;
                } else if (data.Email == inputLogin.Email && decrypPass == false)
                {
                    return -401;
                }
                else
                {
                    return -402;
                }
            } else
            {
                return 0;
            }
        }

        public int ForgotPasword(ForgotVM forgotVM)
        {
            Random rdm = new Random();
            var codeReset = rdm.Next(1, 100000);
            var fixCode = codeReset.ToString().PadLeft(6, '0');

            var dataJoin = context.Employees.Join(context.Accounts,
                e => e.NIK,
                a => a.NIK,
                (e, a) => new
                {
                    NIK = e.NIK,
                    Email = e.Email,
                    Password = a.Password
                }).SingleOrDefault(d => d.Email == forgotVM.Email);

            if (dataJoin != null)
            {
                var dataUpdate = new Account()
                {
                    NIK = dataJoin.NIK,
                    Password = dataJoin.Password,
                    ExpiredToken = DateTime.Now.AddMinutes(5),
                    IsUsed = false,
                    OTP = Convert.ToInt32(fixCode)
                };

                context.Entry(dataUpdate).State = EntityState.Modified;
                var save = context.SaveChanges();

                if (save == 1)
                {
                    try
                    {
                        MimeMessage email = new MimeMessage();
                        email.From.Add(new MailboxAddress("OTP Forgot Password", "allgenre32@gmail.com"));
                        email.To.Add(MailboxAddress.Parse(forgotVM.Email));
                        email.Subject = "OTP Forgot Password Tester";
                        email.Body = new TextPart("Plain") { Text = $"Kode OTP {dataUpdate.OTP}" };

                        SmtpClient smtp = new SmtpClient();
                        smtp.Connect("smtp.gmail.com", 465, true);
                        smtp.Authenticate("allgenre32@gmail.com", "bko132kc97");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        smtp.Dispose();

                        return 1;

                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
                else
                {
                    return -100;
                }
            }
            else
            {
                return -101;
            }

        }

        public int ChagePassword(ChangeVM change)
        {
            var dataJoin = context.Employees.Join(context.Accounts,
                e => e.NIK,
                a => a.NIK,
                (e, a) => new
                {
                    NIK = e.NIK,
                    Email = e.Email,
                    Password = a.Password,
                    Kode = a.OTP,
                    Timeout = a.ExpiredToken,
                    IsUsed = a.IsUsed
                }).SingleOrDefault(d => d.Email == change.Email);
            
            if (dataJoin != null)
            {
                if(dataJoin.Kode != change.OTP)
                {
                    return -101;
                }
                else
                {
                    if(dataJoin.Timeout >= DateTime.Now)
                    {
                        if(dataJoin.IsUsed == true)
                        {
                            return -400;
                        }
                        else
                        {
                            if (change.NewPassword != change.ConfirmPassword)
                            {
                                return -102;
                            }
                            else
                            {
                                Account acc = new Account()
                                {
                                    NIK = dataJoin.NIK,
                                    Password = BCrypt.Net.BCrypt.HashPassword(change.NewPassword),
                                    ExpiredToken = dataJoin.Timeout,
                                    OTP = dataJoin.Kode,
                                    IsUsed = true
                                };

                                context.Entry(acc).State = EntityState.Modified;
                                var save = context.SaveChanges();
                                return save;
                            }
                        }
                    }else
                    {
                        return -500;
                    }
                }
            }
            else
            {
                return -100;
            }
            
        }



        public LoginVM GetUserData(string email)
        {
            var dataRole = (from e in context.Employees
                            join a in context.Accounts on e.NIK equals a.NIK
                            join ar in context.accountRole on a.NIK equals ar.NIK
                            join r in context.role on ar.RoleId equals r.Id
                            where e.Email == email
                            select r.Name).ToArray();

            //return new AccRoleVM
            //{
            //    Email = email,
            //    Roles = dataRole.name
            //};

            //var data = context.Employees.Join(context.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a })
            //.Join(context.accountRole, ea => ea.a.NIK, ar => ar.NIK, (ea, ar) => new { ea, ar })
            //.Join(context.role, accRol => accRol.ar.Id, r => r.Id, (accRol, r) => new { accRol, r })
            //.Select(d => new
            //{
            //    Email = d.accRol.ea.e.Email,
            //    NIK = d.accRol.ea.e.NIK,
            //    Role = d.r.Name
            //}).Where(e => e.Email == email);

            LoginVM acc = new LoginVM
            {
                Email = email,
                Roles = dataRole
            };

            return acc;
        }
    }
}
