using Ekmob.TechSession.Producer.Entites;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Ekmob.TechSession.Producer.Data
{
    public class EmployeeContextSeed
    {
        public static void SeedData(IMongoCollection<Employee> employeeCollection, 
            IMongoCollection<Department> departmentCollection)
        {
            bool existEmployee = employeeCollection.Find(p => true).Any();
            if (!existEmployee)
            {
                bool existDepartment = departmentCollection.Find(p => true).Any();
                if(existDepartment)
                {
                    string departmentId = departmentCollection.Find(p => true).FirstOrDefaultAsync().Result.Id;
                    employeeCollection.InsertManyAsync(GetConfigureEmployees(departmentId));
                }
                else
                {
                    string departmentId = "";
                    employeeCollection.InsertManyAsync(GetConfigureEmployees(departmentId));
                }

            }

        }

        private static IEnumerable<Employee> GetConfigureEmployees(string departmentId)
        {
            return new List<Employee>()
            {
                  new Employee()
                {
                    Name = "Kubilay Özyalçın",
                    IdentityNumber = "77788899900",
                    Email = "kubilay.ozyalcin@ekmob.com",
                    DepartmentId = departmentId,
                    JobTitle = "Backend Developer"
                },
                  new Employee()
                {
                    Name = "İbrahim Kılıç",
                    IdentityNumber = "77788899900",
                    Email = "ibrahim.kilic@ekmob.com",
                    DepartmentId = "1",
                    JobTitle = "Team Lead"
                },

            };
        }
    }
}
