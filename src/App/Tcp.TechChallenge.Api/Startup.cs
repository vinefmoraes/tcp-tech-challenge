using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Tcp.TechChallenge.Domain.Conversion;
using Tcp.TechChallenge.Domain.Conversion.Support;
using Tcp.TechChallenge.Domain.Models;
using Tcp.TechChallenge.Domain.Services;
using Tcp.TechChallenge.Domain.Services.Impl;
using Tcp.TechChallenge.Domain.Validations;
using Tcp.TechChallenge.Domain.Validations.Support;
using Tcp.TechChallenge.Infra.Context;
using Tcp.TechChallenge.Infra.Repositories;
using Tcp.TechChallenge.Infra.Repositories.Impl;

namespace Tcp.TechChallenge.Api
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
            services.AddControllers();
            //services.AddControllersWithViews();
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TCP TechChallenge - API",
                    Version = "v1"
                });
            });

            //InMemory
            services.AddDbContext<ConteinerContext>(options =>
                options.UseInMemoryDatabase("TcpDatabase"));

            //SqlServer
            //services.AddDbContext<ConteinerContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("TcpDatabase")));

            services.AddScoped<IConteinerHandleService, ConteinerHandleService>();
            services.AddScoped<IConteinerRepository, ConteinerRepository>();

            services.AddSingleton<IConverterService, ConverterService>();
            services.AddSingleton<IConverter<ConteinerRequest, Infra.Models.Conteiner>, ConteinerRequestToConteinerParamsConverter>();
            services.AddSingleton<IConverter<Infra.Models.Conteiner, ConteinerRequest>, ConteinerParamsToConteinerRequestConverter>();
            services.AddSingleton<IConverter<IList<Infra.Models.Conteiner>, IList<ConteinerRequest>>, ConteinerResponseToConteinerResultConverter> ();

            services.AddSingleton<IValidationService, ValidationService>();
            services.AddSingleton<IValidator<ConteinerRequest>, ConteinerValidation>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tcp.TechChallenge.Api");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller}/{action=Index}/{id?}");
            });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }
    }
}
