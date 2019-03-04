using System.Threading.Tasks;

namespace App.Common.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();
    }
}
