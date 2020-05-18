using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository.Parameters;

namespace CoastlineServer.Repository
{
    public class UserRepository : RepositoryBase
    {

        public UserRepository(CoastlineContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAll()
        {
            return await Context.Users
                .Include(u => u.StudyGroups)
                .Include(u => u.Strengths)
                .Include(u => u.Members)
                .Include(u => u.Confirmations)
                .ToListAsync();
        }

        public async Task<List<User>> GetAll(UserResourceParameters userResourceParameters)
        {
            if (!CheckGetAllParameters(userResourceParameters))
            {
                return await GetAll();
            }
            
            var collection = Context.Users as IQueryable<User>;

            if (!int.TryParse(userResourceParameters.Strength.Trim(), out var moduleId))
            {
                throw new KeyNotFoundException();
            }

            collection = collection.Where(u => u.Strengths.Any(s => s.ModuleId == moduleId));

            return await collection.Include(u => u.Strengths).ToListAsync();
        }

        public async Task<User> Get(string primaryKey)
        {
            try
            {
                return await Context.Users
                    .Include(u => u.StudyGroups)
                    .Include(u => u.Strengths)
                    .Include(u => u.Members)
                    .Include(u => u.Confirmations)
                    .SingleAsync(u => u.Id == primaryKey);
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message, ex);
            }
        }

        public async Task<User> Insert(User user)
        {
            Context.Entry(user).State = EntityState.Added;
            await Context.SaveChangesAsync();

            return user;
        }

        public async Task Update(User user)
        {
            try
            {
                Context.Entry(user).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public async Task Delete(User user)
        {
            Context.Entry(user).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }
        
        private bool CheckGetAllParameters(UserResourceParameters userResourceParameters)
        {
            if (userResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(userResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(userResourceParameters.Strength))
            {
                return false;
            }

            return true;
        }
    }
}