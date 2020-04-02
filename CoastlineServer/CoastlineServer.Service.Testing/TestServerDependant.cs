using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace CoastlineServer.Service.Testing
{
    public class TestServerDependent : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        public TestServer TestServer => _fixture.Server;
        public HttpClient Client => _fixture.Client;

        public TestServerDependent(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        protected TService GetService<TService>()
            where TService : class
        {
            return _fixture.GetService<TService>();
        }
    }

    public class TestServerFixture : IDisposable
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            // UseStaticRegistration is needed to workaround AutoMapper double initialization. Remove if you don't use AutoMapper.
           // ServiceCollectionExtensions.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()) = false;

            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            Server = new TestServer(hostBuilder);
            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }

        public TService GetService<TService>()
            where TService : class
        {
            return Server?.Host?.Services?.GetService(typeof(TService)) as TService;
        }
    }
}
