using App.Domain.Entities;
using App.Domain.Repositories;
using App.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDataContext _context;

        public UserRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAsync()
        {
            var user = await _context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return user;
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            var users = await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Equals(email)
                    && x.Password.Equals(password));

            return users;
        }

        public async Task<User> GetByEmailAsync(string param)
        {
            return await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Equals(param));
        }

        public async Task<User> GetByIdAsync(Guid param)
        {
            return await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(param));
        }

        public async Task SaveAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
        }

        public async Task UpdateAsync(User obj)
        {
            _context.Entry(obj).State = EntityState.Modified;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User obj)
        {
            _context.Remove(obj);
            await Task.CompletedTask;
        }
    }
}
