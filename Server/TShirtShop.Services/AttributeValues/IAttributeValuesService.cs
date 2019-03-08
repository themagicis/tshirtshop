using System.Collections.Generic;
using System.Threading.Tasks;
using TShirtShop.Services.Results;

namespace TShirtShop.Services.AttributeValues
{
    public interface IAttributeValuesService
    {
        Task<IEnumerable<AttributeValueDto>> GetAllAsync(string attributeName);
        Task<ServiceResult<AttributeValueDto>> GetAsync(int id);
        Task<ServiceResult> CreateAsync(AttributeValueDto item);
        Task<ServiceResult> UpdateAsync(int id, AttributeValueDto item);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
