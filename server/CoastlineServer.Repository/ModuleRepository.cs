using System.Collections.Generic;
using System.Threading.Tasks;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.Repository
{
    public class ModuleRepository : RepositoryBase
    {
        public ModuleRepository(CoastlineContext context) : base(context)
        {
        }

        public async Task<List<Module>> GetAll()
        {
            return await Context.Modules
                .Include(m => m.StudyGroups)
                .Include(m => m.Strengths)
                .ToListAsync();
        }
    }
}