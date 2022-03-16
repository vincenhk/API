
using API.Context;
using API.Models;
using API.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository <MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }


        public IEnumerable GetAllData()
        {
            List<Employee> employees = context.Employees.ToList();
            List<Profiling> profilings = context.Profilings.ToList();
            List<Education> educations = context.Educations.ToList();
            List<University> universities = context.Universities.ToList();

            var employeeRecord = from e in employees
                                 join p in profilings on e.NIK equals p.NIK into table1
                                 from p in table1.ToList()
                                 join ed in educations on p.Education_id equals ed.Id into table2
                                 from ed in table2.ToList()
                                 join u in universities on ed.University_Id equals u.Id into table3
                                 from u in table3.ToList()
                                 select new TestModel
                                 {
                                     NIK = e.NIK,
                                     FullName = $"{e.FirstName} {e.LastName}",
                                     Phone = e.Phone,
                                     Gender = e.Gender.ToString(),
                                     Email = e.Email,
                                     BirthDate = e.BirthDate,
                                     Salary = e.Salary,
                                     Education_id = p.Education_id,
                                     GPA = ed.GPA,
                                     Degree = ed.Degree,
                                     UniversityName = u.Name
                                 };

            return employeeRecord;

            //var employeeRecord = context.Employees.
            //    Join(context.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a })
            //    .Join(context.Profilings, empAcc => empAcc.a.NIK, p => p.NIK, (empAcc, p) => new { empAcc, p })
            //    .Join(context.Educations, eAccPr => eAccPr.p.Education_id, ed => ed.Id, (eAccPr, ed) => new { eAccPr, ed })
            //    .Join(context.Universities, eAcPru => eAcPru.ed.University_Id, u => u.Id, (eAccPru, u) => new { eAccPru, u })
            //    .Select(d => new
            //    {
            //        NIK = d.eAccPru.eAccPr.empAcc.e.NIK,
            //        FullName = $"{d.eAccPru.eAccPr.empAcc.e.FirstName} {d.eAccPru.eAccPr.empAcc.e.LastName}",
            //        Phone = d.eAccPru.eAccPr.empAcc.e.Phone,
            //        Gender = d.eAccPru.eAccPr.empAcc.e.Gender,
            //        Email = d.eAccPru.eAccPr.empAcc.e.Email,
            //        BirthDate = d.eAccPru.eAccPr.empAcc.e.BirthDate,
            //        Salary = d.eAccPru.eAccPr.empAcc.e.Salary,
            //        Education_id = d.eAccPru.eAccPr.p.Education_id,
            //        GPA = d.eAccPru.ed.GPA,
            //        Degree = d.eAccPru.ed.Degree,
            //        UniversityName = d.u.Name
            //    });

            //return employeeRecord;
        }
    }
}
