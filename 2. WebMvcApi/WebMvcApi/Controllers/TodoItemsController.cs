using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase {
        private readonly IRepository<TodoItem> _repository;

        // Dependecy injection of repository
        public TodoItemsController(IRepository<TodoItem> repository) {
            _repository = repository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems() {
            return await _repository.GetItemsAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem?>> GetTodoItem(int id) {
            var itemResult = await _repository.GetItemAsync(id);

            if (itemResult != null)
                return itemResult;

            return NotFound();
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item) {
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

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item) {
            _repository.Add(item);

            await _repository.SaveAsync();

            return CreatedAtAction("GetTodoItem", new { id = item.Id }, item);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id) {
            var itemFound = await _repository.GetItemAsync(id);

            if (itemFound == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            await _repository.SaveAsync();

            return Ok();
        }
    }
}
