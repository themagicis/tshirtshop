using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TShirtShop.Services.Attributes;
using TShirtShop.Services.AttributeValues;

namespace TShittShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(Roles = "Admin")]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributesService svc;
        private readonly IAttributeValuesService valuesSvc;

        public AttributesController(IAttributesService service, IAttributeValuesService valuesService)
        {
            svc = service;
            valuesSvc = valuesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> Get()
        {
            var result = await svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{name}/values")]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> Get(string name)
        {
            var result = await valuesSvc.GetAllAsync(name);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AttributeDto request)
        {
            var result = await svc.CreateAsync(request);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]AttributeDto request)
        {
            var result = await svc.UpdateAsync(id, request);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await svc.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
