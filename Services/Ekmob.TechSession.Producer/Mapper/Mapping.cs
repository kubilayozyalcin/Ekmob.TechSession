using AutoMapper;
using Ekmob.TechSession.RabbitMQ.Events;
using Ekmob.TechSession.Producer.Entites;

namespace Ekmob.TechSession.Producer.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CustomerCreateEvent, Employee>().ReverseMap();
        }
    }
}
