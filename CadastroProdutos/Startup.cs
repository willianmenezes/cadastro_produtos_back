using System;
using System.IO;
using AutoMapper;
using CadastroProduto.Business.Services;
using CadastroProduto.Business.Services.Interfaces;
using CadastroProduto.Data.Context;
using CadastroProduto.Data.Repository;
using CadastroProduto.Data.Structure.Repository;
using CadastroProduto.Library.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace CadastroProdutos
{
    /// <summary>
    /// Class Startup
    /// </summary>
    public class Startup
    {

        private const string DefaultCorsPolicy = "DefaultCorsPolicy";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration properties for application
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method to Configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EntityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")));

            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
            });

            services.AddMvcCore().AddApiExplorer().AddNewtonsoftJson();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            ConfigureSwagger(services);
            ConfigureCors(services);
            ConfigureBusinessServices(services);
            ConfigureRepository(services);
            ConfigureAutoMapper(services);
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mappingConfig.CompileMappings();

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        private void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private void ConfigureBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
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

        /// <summary>
        /// Method to configure properties
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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

            app.UseCors(DefaultCorsPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "CadastroProdutos");
            });
        }
    }
}
