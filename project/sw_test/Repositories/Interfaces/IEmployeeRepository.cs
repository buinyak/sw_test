using sw_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee Create(Employee test);
        void Delete(int id);
        Employee Get(int id);
        IEnumerable<Employee> GetByCompanyId(int id);
        IEnumerable<Employee> GetByDepartmentId(int id);
        Employee Update(Employee test);
    }
}
