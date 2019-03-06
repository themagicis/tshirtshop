using System.Collections.Generic;
using System.Threading.Tasks;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Departments
{
    public interface IDepartmentsService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<ServiceResult<DepartmentDto>> GetAsync(int id);
        Task<ServiceResult> CreateAsync(DepartmentDto item);
        Task<ServiceResult> UpdateAsync(int id, DepartmentDto item);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
