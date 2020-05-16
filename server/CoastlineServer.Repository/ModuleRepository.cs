using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.Repository
{
    public class ModuleRepository : RepositoryBase
    {
        private readonly CoastlineContext _context;

        public ModuleRepository(CoastlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Module>> GetAll()
        {
            return await _context.Modules
                .Include(m => m.StudyGroups)
                .Include(m => m.Strengths)
                .ToListAsync();
        }
    }
}