using AutoMapper;
using Ekmob.TechSession.Application.Commands.CustomerCreate;
using Ekmob.TechSession.Application.Responses;
using Ekmob.TechSession.Domain.Entities;
using Ekmob.TechSession.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Application.Handlers
{
    public class CustmerCreateHandler : IRequestHandler<CustomerCreateCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustmerCreateHandler(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var employeeEntity = _mapper.Map<Customer>(request);
            if (employeeEntity == null)
                throw new ApplicationException("Entity could not be mapped!");

            var employee = await _customerRepository.AddAsync(employeeEntity);

            var employeeResponse = _mapper.Map<CustomerResponse>(employee);

            return employeeResponse;
        }
    }
}
