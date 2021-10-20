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
            var orderEntity = _mapper.Map<Customer>(request);
            if (orderEntity == null)
                throw new ApplicationException("Entity could not be mapped!");

            var order = await _customerRepository.AddAsync(orderEntity);

            var orderResponse = _mapper.Map<CustomerResponse>(order);

            return orderResponse;
        }
    }
}
