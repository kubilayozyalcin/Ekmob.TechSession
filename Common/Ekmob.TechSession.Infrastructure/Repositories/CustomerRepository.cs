using Microsoft.EntityFrameworkCore;
using Ekmob.TechSession.Domain.Entities;
using Ekmob.TechSession.Domain.Repositories;
using Ekmob.TechSession.Infrastructure.Data;
using Ekmob.TechSession.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Customer>> GetCustomerByName(string name)
        {
            var employeeList = await _dbContext.Customers
                      .Where(o => o.Name == name)
                      .ToListAsync();

            return employeeList;
        }
    }
}
