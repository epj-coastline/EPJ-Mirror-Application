using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoastlineServer.DAL.Context;
using CoastlineServer.DAL.Entities;

namespace CoastlineServer.Repository
{
    public class UserRepository
    {
        private readonly CoastlineContext _context;

        public UserRepository(CoastlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(int primaryKey)
        {
            return _context.Users.Single(u => u.Id == primaryKey);
        }

        public User Insert(User user)
        {
            _context.Entry(user).State = EntityState.Added;
            _context.SaveChanges();

            return user;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}