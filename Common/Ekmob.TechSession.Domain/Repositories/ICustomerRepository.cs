using Ekmob.TechSession.Domain.Entities;
using Ekmob.TechSession.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomerByName(string userName);
    }
}
