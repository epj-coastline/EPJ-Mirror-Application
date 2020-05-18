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
    public class UserRepository
    {
        private readonly CoastlineContext _context;

        public UserRepository(CoastlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users
                .Include(u => u.StudyGroups)
                .Include(u => u.Strengths)
                .Include(u => u.Members)
                .Include(u => u.Confirmations)
                .ToListAsync();
        }

        public async Task<List<User>> GetAll(UserResourceParameters userResourceParameters)
        {
            if (userResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(userResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(userResourceParameters.Strength))
            {
                return await GetAll();
            }

            var collection = _context.Users as IQueryable<User>;

            if (!string.IsNullOrWhiteSpace(userResourceParameters.Strength))
            {
                if (!int.TryParse(userResourceParameters.Strength.Trim(), out var moduleId))
                {
                    throw new KeyNotFoundException();
                }

                collection = collection.Where(u => u.Strengths.Any(s => s.ModuleId == moduleId));
            }

            return await collection.Include(u => u.Strengths).ToListAsync();
        }

        public async Task<User> Get(string primaryKey)
        {
            try
            {
                return await _context.Users
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
            _context.Entry(user).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task Update(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public async Task Delete(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}