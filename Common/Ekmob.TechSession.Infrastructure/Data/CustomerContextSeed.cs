using Ekmob.TechSession.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Infrastructure.Data
{
    public class CustomerContextSeed
    {
        public static async Task SeedAsync(CustomerContext orderContext)
        {
            if (!orderContext.Customers.Any())
            {
                orderContext.Customers.AddRange(GetPreconfiguredOrders());

                await orderContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Customer> GetPreconfiguredOrders()
        {
            return new List<Customer>()
            {
               new Customer()
               {
                   Name= "Kubilay Özyalçın",
                   Email = "kubilay.ozyalcin@ekmob.com",
                   DepartmentName = "Backend",
                   IdentityNumber = "22233366688",
                   JobTitle = "Backend Developer",
                   CreateDate = DateTime.Now.ToString()
               },              
               new Customer()
               {
                  Name= "Semanur Karaçam",
                   Email = "semanur.karacam@ekmob.com",
                   DepartmentName = "Backend",
                   IdentityNumber = "11122266688",
                   JobTitle = "Backend Developer",
                   CreateDate = DateTime.Now.ToString()
               }
            };
        }
    }
}
