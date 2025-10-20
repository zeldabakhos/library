using Microsoft.AspNetCore.Mvc;
using project.DTOs;
using project.Services;

namespace project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorController(AuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAuthors();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAuthorById(id);
            if (result == null)
                return NotFound("Author not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthorCreateDTO dto)
        {
            var message = await _service.AddAuthor(dto);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AuthorCreateDTO dto)
        {
            var message = await _service.UpdateAuthor(id, dto);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _service.DeleteAuthor(id);
            return Ok(message);
        }
    }
}