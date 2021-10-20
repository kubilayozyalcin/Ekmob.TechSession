using Microsoft.EntityFrameworkCore;
using Ekmob.TechSession.Domain.Entities;

namespace Ekmob.TechSession.Infrastructure.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

    }
}
