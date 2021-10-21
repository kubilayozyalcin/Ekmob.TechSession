using AutoMapper;
using Ekmob.TechSession.Application.Responses;
using Ekmob.TechSession.Domain.Entities;
using Ekmob.TechSession.Application.Commands.CustomerCreate;

namespace Ekmob.TechSession.Application.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerCreateCommand>().ReverseMap();
            CreateMap<Customer, CustomerResponse>().ReverseMap();
        }
    }
}
