using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Data.Concrete;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Ekmob.TechSession.Producer.Services.Concrete;
using Ekmob.TechSession.Producer.Settings;
using Ekmob.TechSession.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using AutoMapper;

namespace Ekmob.TechSession.Producer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Configuration Dependencies
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            #endregion

            #region Project Dependencies
            services.AddTransient<ISourcingContext, SourcingContext>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IDepartmentService, DepartmentService>();

            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region EventBus
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:UserName"]))
                    factory.UserName = Configuration["EventBus:UserName"];

                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:Password"]))
                    factory.UserName = Configuration["EventBus:Password"];

                var retryCount = 5;

                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:RetryCount"]))
                    retryCount = int.Parse(Configuration["EventBus:RetryCount"]);

                return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ekmob.TechSession.Producer", Version = "v1" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ekmob.TechSession.Producer v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
