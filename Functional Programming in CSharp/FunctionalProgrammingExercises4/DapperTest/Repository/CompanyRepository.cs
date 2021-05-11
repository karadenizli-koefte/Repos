using Dapper;
using DapperTest.Data;
using DapperTest.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperTest.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection _db;
        public CompanyRepository(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Company Add(Company company)
        {
            var sql = @"INSERT INTO [dbo].[Companies] ([Name] ,[Address] ,[City] ,[State] ,[PostalCode])
                               VALUES (@Name, @Address, @City, @State, @PostalCode)
                               SELECT CAST(SCOPE_IDENTITY() as int)";
            //var id = _db.Query<int>(sql, new
            //{
            //    @Name = company.Name,
            //    @Address = company.Address,
            //    @City = company.City,
            //    @State = company.State,
            //    @PostalCode = company.PostalCode
            //});

            var id = _db.Query<int>(sql, new
            {
                company.Name,
                company.Address,
                company.City,
                company.State,
                company.PostalCode
            }).Single();

            company.CompanyId = id;

            return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            return _db.Query<Company>(sql, new { @CompanyId=id}).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return _db.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @CompanyId";

            _db.Execute(sql, new { @CompanyId = id });
        }

        public Company Update(Company company)
        {
            var sql = @"UPDATE Companies
                        SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode
                        WHERE CompanyId = @CompanyId";

            _db.Execute(sql, company);

            return company;
        }
    }
}
