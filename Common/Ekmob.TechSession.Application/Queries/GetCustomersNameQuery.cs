using Ekmob.TechSession.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace Ekmob.TechSession.Application.Queries
{
    public class GetCustomersNameQuery : IRequest<IEnumerable<CustomerResponse>>
    {
        public string Name { get; set; }

        public GetCustomersNameQuery(string name)
        {
            Name = name;
        }
    }
}
