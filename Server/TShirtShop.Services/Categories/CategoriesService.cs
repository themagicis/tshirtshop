using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TShirtShop.DataAccess;
using TShirtShop.DataAccess.Models;
using TShirtShop.Services.Constants;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IAppContext ctx;

        public CategoriesService(IAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<ServiceResult> CreateAsync(CategoryDto item)
        {
            var newEntity = new Category
            {
                Name = item.Name
            };

            ctx.Get<Category>().Add(newEntity);
            await ctx.CommitAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await ctx.Get<Category>().All.FirstOrDefaultAsync(e => e.CategoryId == id);
            if (entity != null)
            {
                ctx.Get<Category>().Remove(entity);
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchCategory));
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var all = await ctx.Get<Category>().All.ToListAsync();
            var retval = all.Select(e => new CategoryDto
            {
                Id = e.CategoryId,
                Name = e.Name,
            }).ToList();

            return retval;
        }

        public async Task<ServiceResult<CategoryDto>> GetAsync(int id)
        {
            var entity = await ctx.Get<Category>().All.FirstOrDefaultAsync(e => e.CategoryId == id);
            if (entity != null)
            {
                var retval = new CategoryDto
                {
                    Id = entity.CategoryId,
                    Name = entity.Name,
                    Description = entity.Description
                };

                return ServiceResult<CategoryDto>.Success(retval);
            }

            return ServiceResult<CategoryDto>.Failed(new ResultError(ErrorMessages.NoSuchCategory));
        }

        public async Task<ServiceResult> UpdateAsync(int id, CategoryDto item)
        {
            var entity = await ctx.Get<Category>().All.FirstOrDefaultAsync(e => e.CategoryId == id);
            if (entity != null)
            {
                entity.Name = item.Name;
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchCategory));
        }
    }
}
