using Dapper;
using Microsoft.Extensions.Configuration;
using sw_test.Models;
using sw_test.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly IConfiguration _configuration;
        readonly string connectionString;
        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }
        public Department Create(Department department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO Departments (Name,Phone) VALUES (@Name,@Phone);SELECT SCOPE_IDENTITY();";
                department.Id = db.Query<int>(sqlQuery, department).FirstOrDefault();
                return department;
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Departments WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Department Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Department>("SELECT * FROM Departments WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Department> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Department>("SELECT * FROM Departments").ToList();
            }
        }

        public Department Update(Department department)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Employees SET Name = @Name, Phone = @Phone WHERE Id = @Id";
                db.Execute(sqlQuery, department);
                return department;
            }
        }
    }
}
