using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ekmob.TechSession.Infrastructure.Data;
using System;

namespace Ekmob.TechSession.Consumer.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();

                    if(customerContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        customerContext.Database.Migrate();
                    }

                    CustomerContextSeed.SeedAsync(customerContext).Wait();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return host;
        }
    }
}
