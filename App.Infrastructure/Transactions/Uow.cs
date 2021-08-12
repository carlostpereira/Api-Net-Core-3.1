using App.Infrastructure.Contexts;
using System.Threading.Tasks;

namespace App.Infrastructure.Transactions
{
    public class Uow : IUow
    {
        private readonly ApplicationDataContext _context;

        public Uow(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
