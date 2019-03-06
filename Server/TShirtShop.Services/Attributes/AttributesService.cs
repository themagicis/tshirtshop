using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TShirtShop.DataAccess;
using TShirtShop.DataAccess.Models;
using TShirtShop.Services.Constants;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Attributes
{
    public class AttributesService : IAttributesService
    {
        private readonly IAppContext ctx;

        public AttributesService(IAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<ServiceResult> CreateAsync(AttributeDto item)
        {
            var newEntity = new Attributte
            {
                Name = item.Name
            };

            ctx.Get<Attributte>().Add(newEntity);
            await ctx.CommitAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await ctx.Get<Attributte>().All.FirstOrDefaultAsync(e => e.AttributeId == id);
            if (entity != null)
            {
                ctx.Get<Attributte>().Remove(entity);
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchAttribute));
        }

        public async Task<IEnumerable<AttributeDto>> GetAllAsync()
        {
            var all = await ctx.Get<Attributte>().All.ToListAsync();
            var retval = all.Select(e => new AttributeDto
            {
                Id = e.AttributeId,
                Name = e.Name,
            }).ToList();

            return retval;
        }

        public async Task<ServiceResult<AttributeDto>> GetAsync(int id)
        {
            var entity = await ctx.Get<Attributte>().All.FirstOrDefaultAsync(e => e.AttributeId == id);
            if (entity != null)
            {
                var retval = new AttributeDto
                {
                    Id = entity.AttributeId,
                    Name = entity.Name
                };

                return ServiceResult<AttributeDto>.Success(retval);
            }

            return ServiceResult<AttributeDto>.Failed(new ResultError(ErrorMessages.NoSuchAttribute));
        }

        public async Task<ServiceResult> UpdateAsync(int id, AttributeDto item)
        {
            var entity = await ctx.Get<Attributte>().All.FirstOrDefaultAsync(e => e.AttributeId == id);
            if (entity != null)
            {
                entity.Name = item.Name;
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchAttribute));
        }
    }
}
