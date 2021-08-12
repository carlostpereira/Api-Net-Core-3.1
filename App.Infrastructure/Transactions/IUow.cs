using System.Threading.Tasks;

namespace App.Infrastructure.Transactions
{
    public interface IUow
    {
        Task Commit();
        Task Rollback();
    }
}
