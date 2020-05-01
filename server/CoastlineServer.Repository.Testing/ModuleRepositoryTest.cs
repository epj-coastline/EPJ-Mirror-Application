using System;
using System.Linq;
using System.Threading.Tasks;
using CoastlineServer.DAL.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoastlineServer.Repository.Testing
{
    public class ModuleRepositoryTest : IDisposable
    {
        private SqliteConnection Connection { get; set; }
        private readonly ModuleRepository _moduleRepository;

        public ModuleRepositoryTest()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
            var options = new DbContextOptionsBuilder<CoastlineContext>()
                .UseSqlite(Connection)
                .Options;
            var context = new CoastlineContext(options);
            context.Database.EnsureCreatedAsync();
            _moduleRepository = new ModuleRepository(context);
        }

        public void Dispose()
        {
            Connection.Close();
        }

        [Fact]
        public async Task GetAll_ReturnsAllModules()
        {
            // arrange & act
            var modules = await _moduleRepository.GetAll();

            Assert.NotEmpty(modules);
            Assert.Contains(modules, m => m.Id == -1);
        }

        [Fact]
        public async Task GetAll_ReturnsAllModulesWithStudyGroups()
        {
            // arrange
            var modules = await _moduleRepository.GetAll();

            // act
            var studyGroups = modules.Single(m => m.Id == -1).StudyGroups;

            // assert
            Assert.NotEmpty(studyGroups);
        }

        [Fact]
        public async Task GetAll_ReturnsAllModulesWithStrenghts()
        {
            // arrange
            var modules = await _moduleRepository.GetAll();

            // act
            var strengths = modules.Single(m => m.Id == -1).Strengths;

            // assert
            Assert.NotEmpty(strengths);
        }
    }
}