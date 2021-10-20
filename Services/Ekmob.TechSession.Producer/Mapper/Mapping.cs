using AutoMapper;
using Ekmob.TechSession.RabbitMQ.Events;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Dtos;

namespace Ekmob.TechSession.Producer.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CustomerCreateEvent, Employee>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();

            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();

            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
        }
    }
}
