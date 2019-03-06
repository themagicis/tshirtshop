using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TShirtShop.DataAccess;
using TShirtShop.DataAccess.Models;
using TShirtShop.Services.Constants;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Departments
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IAppContext ctx;

        public DepartmentsService(IAppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<ServiceResult> CreateAsync(DepartmentDto item)
        {
            var newEntity = new Department
            {
                Name = item.Name,
                Description = item.Description
            };

            ctx.Get<Department>().Add(newEntity);
            await ctx.CommitAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await ctx.Get<Department>().All.FirstOrDefaultAsync(a => a.DepartmentId == id);
            if (entity != null)
            {
                ctx.Get<Department>().Remove(entity);
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchDepartment));
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var all = await ctx.Get<Department>().All.ToListAsync();
            var retval = all.Select(e => new DepartmentDto
            {
                Id = e.DepartmentId,
                Name = e.Name,
                Description = e.Description
            }).ToList();

            return retval;
        }

        public async Task<ServiceResult<DepartmentDto>> GetAsync(int id)
        {
            var attribute = await ctx.Get<Department>().All.FirstOrDefaultAsync(e => e.DepartmentId == id);
            if (attribute != null)
            {
                var retval = new DepartmentDto
                {
                    Id = attribute.DepartmentId,
                    Name = attribute.Name,
                    Description = attribute.Description
                };

                return ServiceResult<DepartmentDto>.Success(retval);
            }

            return ServiceResult<DepartmentDto>.Failed(new ResultError(ErrorMessages.NoSuchDepartment));
        }

        public async Task<ServiceResult> UpdateAsync(int id, DepartmentDto item)
        {
            var entity = await ctx.Get<Department>().All.FirstOrDefaultAsync(e => e.DepartmentId == id);
            if (entity != null)
            {
                entity.Name = item.Name;
                entity.Description = item.Description;
                await ctx.CommitAsync();

                return ServiceResult.Success();
            }

            return ServiceResult.Failed(new ResultError(ErrorMessages.NoSuchDepartment));
        }
    }
}
