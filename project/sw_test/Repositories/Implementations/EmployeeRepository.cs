using Dapper;
using Microsoft.Extensions.Configuration;
using sw_test.Models;
using sw_test.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace sw_test.Repositories.Implementations
{

    public class EmployeeRepository : IEmployeeRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }

        public Employee Create(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO Employees (Name,Surname,Phone,CompanyId,DepartmentId) VALUES(@Name,@Surname,@Phone,@CompanyId,@DepartmentId); " +
                    "SELECT SCOPE_IDENTITY();";
                employee.Id = db.Query<int>(sqlQuery, employee).FirstOrDefault();
                employee.Passport.EmployeeId = employee.Id;
                sqlQuery = "INSERT INTO Passports (EmployeeId,Type,Number) VALUES (@EmployeeId,@Type,@Number)";
                db.Execute(sqlQuery, employee.Passport);

                return employee;

            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Employees WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Employee Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Employee>("SELECT * FROM Employees WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Employee> GetByCompanyId(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employees " +
                    "LEFT JOIN Passports ON Employees.Id = Passports.EmployeeId " +
                    "LEFT JOIN Departments ON Employees.DepartmentId = Departments.Id " +
                    "WHERE Employees.CompanyId = @id";
                return db.Query<Employee, Passport, Department, Employee>(sqlQuery,
                    (e, p, d) =>
                    {
                        e.Department = d;
                        e.Passport = p;
                        return e;
                    }
                    , new { Id = id }).ToList();
            }
        }

        public IEnumerable<Employee> GetByDepartmentId(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employees " +
                    "LEFT JOIN Passports ON Employees.Id = Passports.EmployeeId " +
                    "LEFT JOIN Departments ON Employees.DepartmentId = Departments.Id " +
                    "WHERE Employees.DepartmentId = @id";
                return db.Query<Employee, Passport, Department, Employee>(sqlQuery,
                    (e, p, d) =>
                    {
                        e.Department = d;
                        e.Passport = p;
                        return e;
                    }, new { Id = id }).ToList();
            }
        }

        public Employee Update(Employee employee)
        {
            string sqlQuery = "UPDATE Employees SET ";
            if (employee.Name != null) { sqlQuery += "Name = @Name,"; }
            if (employee.Surname != null) { sqlQuery += "Surname = @Surname,"; }
            if (employee.Phone != null) { sqlQuery += "Phone = @Phone,"; }
            if (employee.CompanyId != null) { sqlQuery += "CompanyId = @CompanyId,"; }
            if (employee.DepartmentId != null) { sqlQuery += "DepartmentId = @DepartmentId"; }
            if (sqlQuery.EndsWith(',')) { sqlQuery.Substring(0, sqlQuery.Length - 1); }
            sqlQuery += " WHERE Id = @id";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute(sqlQuery, employee);
            }
            if (employee.Passport != null)
            {
                sqlQuery = "UPDATE Passports SET ";
                if (employee.Passport.Type != null) { sqlQuery += "Type = @Type,"; }
                if (employee.Passport.Number != null) { sqlQuery += "Number=@Number"; }
                if (sqlQuery.EndsWith(',')) { sqlQuery.Substring(0, sqlQuery.Length - 1); }
                sqlQuery += " WHERE EmployeeId = @EmployeeId";

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    employee.Passport.EmployeeId = employee.Id;
                    db.Execute(sqlQuery, employee.Passport);
                }
            }
            return employee;
        }
    }
}
