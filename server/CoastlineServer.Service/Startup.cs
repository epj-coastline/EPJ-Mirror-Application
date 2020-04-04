using System;
using System.Net.Sockets;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CoastlineServer.DAL.Context;
using CoastlineServer.Repository;

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
            services.AddDbContext<CoastlineContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionStringCoastline"]);
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            PingHost("10.20.0.3", 5432);
            PingHost("10.20.0.3", 7000);
        }

        public static void PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                {
                    Console.WriteLine("_____________ Access________________");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Error pinging host:'" + hostUri + ":" + portNumber.ToString() + "'");
            }
        }
    }
}