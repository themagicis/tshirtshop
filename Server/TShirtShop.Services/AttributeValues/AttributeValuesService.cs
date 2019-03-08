using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TShirtShop.DataAccess;
using TShirtShop.DataAccess.Models;
using TShirtShop.Services.Constants;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.AttributeValues
{
    public class AttributeValuesService : IAttributeValuesService
    {
        private readonly IAppContext ctx;

        public AttributeValuesService(IAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<ServiceResult> CreateAsync(AttributeValueDto item)
        {
            var newEntity = new Attributte
            {
                Name = item.Value
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

        public async Task<IEnumerable<AttributeValueDto>> GetAllAsync(string attributeName)
        {
            var all = await ctx.Get<AttributeValue>().All.Where(a => a.Attribute.Name == attributeName).ToListAsync();
            var retval = all.Select(e => new AttributeValueDto
            {
                Id = e.AttributeValueId,
                Value = e.Value,
            }).ToList();

            return retval;
        }

        public async Task<ServiceResult<AttributeValueDto>> GetAsync(int id)
        {
            var entity = await ctx.Get<Attributte>().All.FirstOrDefaultAsync(e => e.AttributeId == id);
            if (entity != null)
            {
                var retval = new AttributeValueDto
                {
                    Id = entity.AttributeId,
                    Value = entity.Name
                };

                return ServiceResult<AttributeValueDto>.Success(retval);
            }

            return ServiceResult<AttributeValueDto>.Failed(new ResultError(ErrorMessages.NoSuchAttribute));
        }

        public async Task<ServiceResult> UpdateAsync(int id, AttributeValueDto item)
        {
            var entity = await ctx.Get<Attributte>().All.FirstOrDefaultAsync(e => e.AttributeId == id);
            if (entity != null)
            {
                entity.Name = item.Value;
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchAttribute));
        }
    }
}
