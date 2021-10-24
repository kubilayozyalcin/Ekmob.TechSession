using Ekmob.TechSession.Application.Responses;
using MediatR;
using System;

namespace Ekmob.TechSession.Consumer.Entities
{
    public class CustomerCreateCommand : IRequest<CustomerResponse>
    {
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string CreateDate { get; set; }

    }
}
