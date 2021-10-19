using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.Core.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
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
