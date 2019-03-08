using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TShirtShop.Services.Categories;
using TShirtShop.Services.Departments;

namespace TShittShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize(Roles = "Admin")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService svc;
        private readonly ICategoriesService categoriesSvc;

        public DepartmentsController(IDepartmentsService service, ICategoriesService categoriesService)
        {
            svc = service;
            categoriesSvc = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> Get()
        {
            var result = await svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{name}/categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get(string name)
        {
            var result = await categoriesSvc.GetAllAsync(name);
            return Ok(result);
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
