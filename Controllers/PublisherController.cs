using Microsoft.AspNetCore.Mvc;
using project.DTOs;
using project.Services;

namespace project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherService _service;

        public PublisherController(PublisherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllPublishers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetPublisherById(id);
            if (result == null)
                return NotFound("Publisher not found.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PublisherCreateDTO dto)
        {
            var message = await _service.AddPublisher(dto);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PublisherCreateDTO dto)
        {
            var message = await _service.UpdatePublisher(id, dto);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _service.DeletePublisher(id);
            return Ok(message);
        }
    }
}