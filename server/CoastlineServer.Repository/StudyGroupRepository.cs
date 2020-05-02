using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository.Parameters;
using Microsoft.EntityFrameworkCore;

namespace CoastlineServer.Repository
{
    public class StudyGroupRepository : RepositoryBase
    {
        private readonly CoastlineContext _context;

        public StudyGroupRepository(CoastlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<StudyGroup>> GetAll()
        {
            return await _context.StudyGroups
                .Include(s => s.User)
                .Include(s => s.Module)
                .Include(s => s.Members)
                .ToListAsync();
        }

        public async Task<List<StudyGroup>> GetAll(StudyGroupResourceParameters studyGroupResourceParameters)
        {
            if (studyGroupResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(studyGroupResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(studyGroupResourceParameters.Module))
            {
                return await GetAll();
            }

            var collection = _context.StudyGroups as IQueryable<StudyGroup>;

            if (!string.IsNullOrWhiteSpace(studyGroupResourceParameters.Module))
            {
                if (!int.TryParse(studyGroupResourceParameters.Module.Trim(), out var moduleId))
                {
                    throw new KeyNotFoundException(nameof(studyGroupResourceParameters));
                }
                
                collection = collection
                    .Where(s => s.ModuleId == moduleId);
            }

            return await collection
                .Include(s => s.User)
                .Include(s => s.Module)
                .Include(s => s.Members)
                .ToListAsync();
        }

        public async Task<StudyGroup> Get(int primaryKey)
        {
            try
            {
                return await _context.StudyGroups
                    .Include(s => s.User)
                    .Include(s => s.User)
                    .Include(s => s.Members)
                    .SingleAsync(s => s.Id == primaryKey);
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message, ex);
            }
        }

        public async Task<StudyGroup> Insert(StudyGroup studyGroup)
        {
            _context.Entry(studyGroup).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return await _context.StudyGroups.Include(s => s.User)
                .Include(s => s.Module)
                .SingleAsync(s => s.Id == studyGroup.Id);
        }

        public async Task Update(StudyGroup studyGroup)
        {
            try
            {
                _context.Entry(studyGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw CreateOptimisticConcurrencyException(_context, studyGroup);
            }
        }

        public async Task Delete(StudyGroup studyGroup)
        {
            _context.Entry(studyGroup).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}