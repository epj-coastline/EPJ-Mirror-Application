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

        public StudyGroupRepository(CoastlineContext context) : base(context)
        {
        }

        public async Task<List<StudyGroup>> GetAll()
        {
            return await Context.StudyGroups
                .Include(s => s.User)
                .Include(s => s.Module)
                .Include(s => s.Members)
                .ToListAsync();
        }

        public async Task<List<StudyGroup>> GetAll(StudyGroupResourceParameters studyGroupResourceParameters)
        {
            if (!CheckGetAllParameters(studyGroupResourceParameters))
            {
                return await GetAll();
            }

            var collection = Context.StudyGroups as IQueryable<StudyGroup>;


            if (!int.TryParse(studyGroupResourceParameters.Module.Trim(), out var moduleId))
            {
                throw new KeyNotFoundException(nameof(studyGroupResourceParameters));
            }

            collection = collection
                .Where(s => s.ModuleId == moduleId);

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
                return await Context.StudyGroups
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
            Context.Entry(studyGroup).State = EntityState.Added;
            await Context.SaveChangesAsync();

            return await Context.StudyGroups.Include(s => s.User)
                .Include(s => s.Module)
                .SingleAsync(s => s.Id == studyGroup.Id);
        }

        public async Task Update(StudyGroup studyGroup)
        {
            try
            {
                Context.Entry(studyGroup).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public async Task Delete(StudyGroup studyGroup)
        {
            Context.Entry(studyGroup).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }
        
        private bool CheckGetAllParameters(StudyGroupResourceParameters studyGroupResourceParameters)
        {
            if (studyGroupResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(studyGroupResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(studyGroupResourceParameters.Module))
            {
                return false;
            }

            return true;
        }
    }
}