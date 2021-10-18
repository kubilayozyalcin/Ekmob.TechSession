using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Data.Concrete;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Ekmob.TechSession.Producer.Services.Concrete;
using Ekmob.TechSession.Producer.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
