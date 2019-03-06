using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TShirtShop.Services.Departments;

namespace TShittShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService svc;

        public DepartmentsController(IDepartmentsService service)
        {
            svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> Get()
        {
            var result = await svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> Get(int id)
        {
            var result = await svc.GetAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]DepartmentDto request)
        {
            var result = await svc.CreateAsync(request);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]DepartmentDto request)
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
