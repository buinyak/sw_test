using sw_test.Models;
using sw_test.Repositories.Interfaces;
using sw_test.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Services.Implementations
{
    public class DepartmentService : IDepartementService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public Department Create(Department department)
        {
            return _departmentRepository.Create(department);
        }

        public void Delete(int id)
        {
            _departmentRepository.Delete(id);
        }

        public Department Get(int id)
        {
            return _departmentRepository.Get(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department Update(Department department)
        {
            return _departmentRepository.Update(department);
        }
    }
}
