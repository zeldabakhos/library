using Microsoft.AspNetCore.Mvc;
using project.DTOs;
using project.Services;

namespace project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;  

        public BookController(BookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllBooks();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetBookById(id);
            if (result == null)
                return NotFound("Book not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookCreateDTO dto)
        {
            var message = await _service.AddBook(dto);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BookCreateDTO dto)
        {
            var message = await _service.UpdateBook(id, dto);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _service.DeleteBook(id);
            return Ok(message);
        }
    }
}