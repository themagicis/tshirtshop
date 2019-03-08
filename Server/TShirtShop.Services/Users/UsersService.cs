using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TShirtShop.DataAccess;
using TShirtShop.DataAccess.Models;
using TShirtShop.Services.Constants;
using TShirtShop.Services.Helpers;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IAppContext ctx;

        public UsersService(IAppContext ctx)
        {
            this.ctx = ctx;
        }
        public IQueryable<User> All()
        {
            return ctx.Get<User>().All;
        }

        public Task<ServiceResult<User>> CreateAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<User>> GetByIdAsync(int id)
        {
            var user = await All().FirstOrDefaultAsync(a => a.Id == id);

            if (user == null)
            {
                return ServiceResult<User>.Failed(new ResultError(ErrorMessages.NoSuchUser));
            }

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<User>> PasswordSignInAsync(string email, string password)
        {
            var user = await ctx.Get<User>().All.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                var isEqual = PasswordHasher.Verify(password, user.PasswordHash);
                if (isEqual)
                {
                    return ServiceResult<User>.Success(user);
                }
            }

            return ServiceResult<User>.Failed(new ResultError(ErrorMessages.InvalidLogin));
        }
    }
}
