﻿using Ekmob.TechSession.Application.Responses;
using MediatR;

namespace Ekmob.TechSession.Application.Commands.CustomerCreate
{
    public class CustomerCreateCommand : IRequest<CustomerResponse>
    {
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }

        public string CreateDate { get; set; }
    }
}
