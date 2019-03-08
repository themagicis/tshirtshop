using System.Linq;
using System.Threading.Tasks;
using TShirtShop.DataAccess.Models;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Users
{
    public interface IUsersService
    {
        IQueryable<User> All();

        Task<ServiceResult<User>> GetByIdAsync(int id);

        Task<ServiceResult<User>> CreateAsync(User user, string password);

        Task<ServiceResult<User>> PasswordSignInAsync(string email, string password);
    }
}
