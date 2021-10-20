using AutoMapper;
using Ekmob.TechSession.Consumer.Data.Abstractions;
using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.Consumer.Services.Abstractions;
using Ekmob.TechSession.Shared.Utilities.Response;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Consumer.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerContext _customerContext;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerContext customerContext, IMapper mapper)
        {
            _customerContext = customerContext;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerContext.Customers.Find(customers => true).ToListAsync();

            return Response<IEnumerable<Customer>>.Success(_mapper.Map<IEnumerable<Customer>>(customers), 200);
        }

        public async Task<Response<Customer>> GetCustomer(string id)
        {
            var customer = await _customerContext.Customers.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (customer == null)
                return Response<Customer>.Fail("Customer not found", 404);

            return Response<Customer>.Success(_mapper.Map<Customer>(customer), 200);
        }


        public async Task<Response<Customer>> Create(Customer customer)
        {
            var newCustomer = _mapper.Map<Customer>(customer);

            await _customerContext.Customers.InsertOneAsync(customer);

            return Response<Customer>.Success(_mapper.Map<Customer>(customer), 200);
        }

       
      
    }
}
