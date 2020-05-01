using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CoastlineServer.DAL.Context;
using CoastlineServer.Repository;
using Newtonsoft.Json;

namespace CoastlineServer.Service
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<UserRepository>();
            services.AddScoped<StudyGroupRepository>();
            services.AddScoped<ModuleRepository>();
            services.AddDbContext<CoastlineContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStringCoastline"]);
            });
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Coastline API";
                    document.Info.Description = "ASP.NET Core web API for Coastline";
                    document.ExternalDocumentation = new NSwag.OpenApiExternalDocumentation
                    {
                        Description = "Coastline Documentation",
                        Url = "http://epj.pages.ifs.hsr.ch/2020/coastline/documentation/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CoastlineContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration["DatabaseMigrations"] == "automatic")
            {
                context.Database.Migrate();
            }

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration["AllowedOrigin"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseAuthorization();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3(options => { options.Path = ""; });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}