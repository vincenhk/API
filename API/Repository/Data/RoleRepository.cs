using API.Context;
using API.Models;
using API.ViewModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        public readonly MyContext context;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public IEnumerable RegistredEmployee()
        {
            //var emp = (from e in context.Employees
            //           join a in context.Accounts on e.NIK equals a.NIK
            //           join ar in context.accountRole on a.NIK equals ar.NIK
            //           join r in context.role on ar.RoleId equals r.Id where r.Name == "Employee")
            //           .Select(e => new {
            //               NIK = e.NIK,
            //               FristName = e.FristName,

            //           });

            var emp = context.Employees.Join(context.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a })
                .Join(context.accountRole, ea => ea.a.NIK, ar => ar.NIK, (ea, ar) => new { ea, ar })
                .Join(context.role, eaar => eaar.ar.RoleId, r => r.Id, (eaar, r) => new {eaar, r})
                .Select(d => new
                {
                    Email = d.eaar.ea.e.Email,
                    Name = $"{d.eaar.ea.e.FirstName} {d.eaar.ea.e.LastName}",
                    RoleName = d.r.Name
                }).Where(r => r.RoleName == "Employee").ToList();

            return emp.ToList();
        }
    }
}

