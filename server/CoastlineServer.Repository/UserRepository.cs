using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;
using CoastlineServer.Repository.Exceptions;

namespace CoastlineServer.Repository
{
    public class UserRepository : RepositoryBase
    {
        private readonly CoastlineContext _context;

        public UserRepository(CoastlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Get(int primaryKey)
        {
            try
            {
                return await _context.Users.SingleAsync(u => u.Id == primaryKey);
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
            catch (Exception)
            {
                throw CreateOptimisticConcurrencyException(_context, user);
            }
        }

        public async Task Delete(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}