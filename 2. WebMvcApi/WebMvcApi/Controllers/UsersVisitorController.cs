using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Controllers {
    [Route("api/visitor/[controller]")]
    [ApiController]
    public class UsersVisitorController : ControllerBase {
        private readonly UserVisitorRepository _repository;
        public UsersVisitorController(UserVisitorRepository repository) {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers() {
            return await _repository.GetItemsAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(int id) {
            var itemResult = await _repository.GetItemAsync(id);

            if (itemResult != null)
                return itemResult;

            return NotFound();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User item) {
            if (id != item.Id)
                return BadRequest();

            await _repository.UpdateAsync(item);

            try {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException) {
                var itemFound = _repository.GetItemAsync(id);
                if (itemFound == null)
                    return NotFound();
                else
                    throw;
            }

            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User item) {
            _repository.Add(item);

            await _repository.SaveAsync();

            return CreatedAtAction("GetUser", new { id = item.Id }, item);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            await _repository.DeleteAsync(id);

            await _repository.SaveAsync();

            return Ok();
        }
    }
}
