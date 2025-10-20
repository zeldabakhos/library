using Microsoft.AspNetCore.Mvc;
using project.DTOs;
using project.Services;

namespace project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetCategoryById(id);
            if (result == null)
                return NotFound("Category not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDTO dto)
        {
            var message = await _service.AddCategory(dto);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryCreateDTO dto)
        {
            var message = await _service.UpdateCategory(id, dto);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _service.DeleteCategory(id);
            return Ok(message);
        }
    }
}