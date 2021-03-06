using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        virtual public Passport Passport { get; set; }
        virtual public Department Department { get; set; }
    }
}
