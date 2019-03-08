using System.Collections.Generic;
using System.Threading.Tasks;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Categories
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(string departmentName);
        Task<ServiceResult<CategoryDto>> GetAsync(int id);
        Task<ServiceResult> CreateAsync(CategoryDto item);
        Task<ServiceResult> UpdateAsync(int id, CategoryDto item);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
