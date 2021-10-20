using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Data.Concrete
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Departments = database.GetCollection<Department>(settings.DepartmentCollectionName);
            DepartmentContextSeed.SeedData(Departments);

            Employees = database.GetCollection<Employee>(settings.EmployeeCollectionName);
            EmployeeContextSeed.SeedData(Employees, Departments);
        }

        public IMongoCollection<Employee> Employees { get; }
        public IMongoCollection<Department> Departments { get; }
    }
}
