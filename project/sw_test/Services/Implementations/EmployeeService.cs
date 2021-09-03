using sw_test.Models;
using sw_test.Repositories.Interfaces;
using sw_test.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int Create(Employee employee)
        {
           return _employeeRepository.Create(employee).Id;
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }

        public Employee Get(int id)
        {
            return _employeeRepository.Get(id);
        }

        public IEnumerable<Employee> GetByCompanyId(int id)
        {
            return _employeeRepository.GetByCompanyId(id);
        }

        public IEnumerable<Employee> GetByDepartmentId(int id)
        {
            return _employeeRepository.GetByDepartmentId(id);
        }

        public Employee Update(Employee employee)
        {
            return _employeeRepository.Update(employee);
        }
    }
}
