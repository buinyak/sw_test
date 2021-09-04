using sw_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Department Create(Department department);
        void Delete(int id);
        Department Get(int id);
        IEnumerable<Department> GetAll();
        Department Update(Department department);
    }
}
