using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CadastroProdutos
{
    public class Startup
    {

        private const string DefaultCorsPolicy = "DefaultCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
            });

            services.AddMvcCore().AddApiExplorer().AddNewtonsoftJson();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();

            ConfigureSwagger(services);
            ConfigureCors(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(DefaultCorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "default",
                        template: "/api/{culture}/{controller}/{action}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "CadastroProdutos");
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var swaggerXMLPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cadastro de produtos API",
                    Description = "Microserviços para a aplicação de cadastro de produtos de um supermercado",
                    Contact = new OpenApiContact() { Name = "Willian Menezes", Email = "willian_menezes_santos@hotmail.com", Url = new System.Uri("https://www.linkedin.com/in/willian-menezes-9932b1b9") }
                });

                c.IncludeXmlComments(swaggerXMLPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(setup =>
            {
                setup.AddPolicy(DefaultCorsPolicy, builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
        }
    }
}
