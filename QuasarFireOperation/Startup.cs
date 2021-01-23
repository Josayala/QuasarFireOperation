
using Mediator.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuasarFireOperation.Domain.CommandModel.Mappers;
using QuasarFireOperation.Domain.CommandModel.Repositories;
using QuasarFireOperation.Infrastructure.Mappers;
using QuasarFireOperation.Persistence.Sql;
using Swashbuckle.AspNetCore.Swagger;
using Web.Core;

namespace QuasarFireOperation
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public ILogger Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogRequestPerformanceBehavior<,>));
            services.AddMediatR();
            services.AddMemoryCache();
            services
                .AddMvc(options => { options.Filters.Add(new ExceptionFilter(HostingEnvironment, Logger)); })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new JsonContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ISatelliteMapper, SateliteMapper>();


            services.AddScoped<IUnitOfWork, SqlUnitOfWork>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Quasar Fire Operation API", Version = "v1"});
                //c.AddSecurityDefinition("basic", new BasicAuthScheme { Type = "basic" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            string swaggerEndPoint = null;
            swaggerEndPoint = "/swagger/v1/swagger.json";
            app.UseSwaggerUI(c => { c.SwaggerEndpoint(swaggerEndPoint, "SAT API V1"); });

            app.UseMvc();
        }
    }
}