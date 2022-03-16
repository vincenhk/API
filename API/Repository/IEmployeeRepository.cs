using API.Models;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();

        Employee Get(string NIK);
        int Insert(Employee employee);
        int update(Employee employee);
        int delete(string NIK);

    }
}
