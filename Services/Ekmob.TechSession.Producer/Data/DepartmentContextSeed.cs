using Ekmob.TechSession.Producer.Entites;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Ekmob.TechSession.Producer.Data
{
    public class DepartmentContextSeed
    {
        public static void SeedData(IMongoCollection<Department> departmentCollection)
        {
            bool existDepartment = departmentCollection.Find(p => true).Any();
            if (!existDepartment)
            {
                departmentCollection.InsertManyAsync(GetConfigureDepartments());
            }
        }

        private static IEnumerable<Department> GetConfigureDepartments()
        {
            return new List<Department>()
            {
                  new Department()
                {
                    DepartmentName = "Backend Team",
                },
                  new Department()
                {
                    DepartmentName = "Frontend Team",
                },

            };
        }
    }
}
