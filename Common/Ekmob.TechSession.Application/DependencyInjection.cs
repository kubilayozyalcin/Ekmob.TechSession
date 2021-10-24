using AutoMapper;
using Ekmob.TechSession.Application.Commands.CustomerCreate;
using Ekmob.TechSession.Application.Mapping;
using Ekmob.TechSession.Application.PipelineBehaviours;
using Ekmob.TechSession.Application.Responses;
using Ekmob.TechSession.Domain.Repositories;
using Ekmob.TechSession.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Ekmob.TechSession.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(CustomerCreateCommand).GetTypeInfo().Assembly);

            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            #region Configure Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<CustomerMappingProfile>();
            });
            var mapper = config.CreateMapper();
            #endregion

            return services;
        }
    }
}
