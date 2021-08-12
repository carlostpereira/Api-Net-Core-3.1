using App.Domain.Entities;
using App.Domain.Repositories;
using App.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationDataContext _context;

        public ProviderRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Provider>> GetAsync()
        {
            var Provider = await _context
                .Providers
                .AsNoTracking()
                .ToListAsync();

            return Provider;
        }

        public async Task<Provider> GetByNameAsync(string param)
        {
            return await _context
                .Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.Equals(param));
        }

        public async Task<Provider> GetByCnpjAsync(string param)
        {
            return await _context
                .Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Cnpj.Equals(param));
        }

        public async Task<Provider> GetByIdAsync(Guid param)
        {
            return await _context
                .Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(param));
        }

        public async Task SaveAsync(Provider obj)
        {
            await _context.Providers.AddAsync(obj);
        }

        public async Task UpdateAsync(Provider obj)
        {
            _context.Entry(obj).State = EntityState.Modified;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Provider obj)
        {
            _context.Remove(obj);
            await Task.CompletedTask;
        }
    }
}
