using System.Collections.Generic;
using System.Threading.Tasks;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.Attributes
{
    public interface IAttributesService
    {
        Task<IEnumerable<AttributeDto>> GetAllAsync();
        Task<ServiceResult<AttributeDto>> GetAsync(int id);
        Task<ServiceResult> CreateAsync(AttributeDto item);
        Task<ServiceResult> UpdateAsync(int id, AttributeDto item);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
