using AutoMapper;
using Ekmob.TechSession.Application.Responses;
using MediatR;
using Ekmob.TechSession.Application.Queries;
using Ekmob.TechSession.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Application.Handlers
{
    public class GetCustomersByNameHandler : IRequestHandler<GetCustomersNameQuery, IEnumerable<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersByNameHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(GetCustomersNameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _customerRepository.GetCustomerByName(request.Name);

            var response = _mapper.Map<IEnumerable<CustomerResponse>>(orderList);

            return response;
        }
    }
}
