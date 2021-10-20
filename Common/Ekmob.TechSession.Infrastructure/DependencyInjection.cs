using Ekmob.TechSession.Domain.Repositories;
using Ekmob.TechSession.Domain.Repositories.Base;
using Ekmob.TechSession.Infrastructure.Data;
using Ekmob.TechSession.Infrastructure.Repositories;
using Ekmob.TechSession.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ekmob.TechSession.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<CustomerContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("CustomerConnection"),
                        b => b.MigrationsAssembly(typeof(CustomerContext).Assembly.FullName)), ServiceLifetime.Singleton);

            //Add Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
