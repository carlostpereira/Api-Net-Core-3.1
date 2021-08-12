using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Repositories
{
    public interface IProviderRepository
    {
        Task<ICollection<Provider>> GetAsync();
        Task<Provider> GetByNameAsync(string param);
        Task<Provider> GetByCnpjAsync(string param);
        Task<Provider> GetByIdAsync(Guid param);
        Task SaveAsync(Provider obj);
        Task UpdateAsync(Provider obj);
        Task DeleteAsync(Provider obj);

    }
}
