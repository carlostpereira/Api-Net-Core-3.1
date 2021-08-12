using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAsync();
        Task<User> LoginUserAsync(string email, string password);
        Task<User> GetByEmailAsync(string param);
        Task<User> GetByIdAsync(Guid param);
        Task SaveAsync(User obj);
        Task UpdateAsync(User obj);
        Task DeleteAsync(User obj);

    }
}
