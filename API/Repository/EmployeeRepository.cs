using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly MyContext context;
        public int delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);

            if (entity == null)
            {
                return -100;
            }
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            var result = context.Employees.Find(NIK);
            return result;
        }

        public int Insert(Employee employee)
        {
            DateTime date = DateTime.Now;
            var checkEmail = context.Employees.Where(e => e.Email == employee.Email).SingleOrDefault();
            var checkPhone = context.Employees.Where(e => e.Phone == employee.Phone).SingleOrDefault();
            var NIK = $"{date.ToString("yyyy")}00{Get().Count()}";

            int makeNIK = Convert.ToInt32(NIK)+1;

            if (checkEmail != null && checkPhone != null)
            {
                return -202;
            }else if(checkEmail != null)
            {
                return -101;
            }else if(checkPhone != null)
            {
                return -102;
            }
            employee.NIK = $"{makeNIK}";
            context.Employees.Add(employee);
            var result = context.SaveChanges();
            return result;
        }

        public int update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}
