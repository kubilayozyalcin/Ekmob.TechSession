using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.Shared.Utilities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Consumer.Services.Abstractions
{
    public interface ICustomerService
    {

        Task<Response<IEnumerable<Customer>>> GetCustomers();
        Task<Response<Customer>> GetCustomer(string id);
        Task<Response<Customer>> Create(Customer customer);

    }
}
