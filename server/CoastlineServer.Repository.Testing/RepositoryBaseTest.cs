using System;
using CoastlineServer.DAL.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.Repository.Testing
{
    public class RepositoryBaseTest : IDisposable
    {
        protected readonly CoastlineContext Context;
        private SqliteConnection Connection { get; set; }

        protected RepositoryBaseTest()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            var options = new DbContextOptionsBuilder<CoastlineContext>()
                .UseSqlite(Connection)
                .Options;
            Context = new CoastlineContext(options);
            Context.Database.EnsureCreatedAsync();
        }
        
        public void Dispose()
        {
            Connection.Close();
        }
    }
}