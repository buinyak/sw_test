using sw_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Services.Interfaces
{
    public interface IEmployeeService
    {
        int Create(Employee employee);
        void Delete(int id);
        Employee Get(int id);
        IEnumerable<Employee> GetByCompanyId(int id);
        IEnumerable<Employee> GetByDepartmentId(int id);
        Employee Update(Employee employee);
    }
}
