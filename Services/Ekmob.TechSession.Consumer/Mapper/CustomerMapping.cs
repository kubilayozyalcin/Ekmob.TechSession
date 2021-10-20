using AutoMapper;
using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.RabbitMQ.Events;

namespace Ekmob.TechSession.Consumer.Mapper
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerCreateEvent, CustomerCreateCommand>().ReverseMap();
        }
    }
}
