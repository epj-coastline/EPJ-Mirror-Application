using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoastlineServer.Repository.Testing
{
    public class ModuleRepositoryTest : RepositoryBaseTest
    {
        private readonly ModuleRepository _moduleRepository;

        public ModuleRepositoryTest()
        {
            _moduleRepository = new ModuleRepository(Context);
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