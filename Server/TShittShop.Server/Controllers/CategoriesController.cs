using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TShirtShop.Services.Categories;

namespace TShittShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService svc;

        public CategoriesController(ICategoriesService service)
        {
            svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var result = await svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var result = await svc.GetAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CategoryDto request)
        {
            var result = await svc.CreateAsync(request);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]CategoryDto request)
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
