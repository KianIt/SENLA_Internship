using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IRepository<TodoItem> _repository;

        // Dependecy injection of repository
        public TodoItemsController(IRepository<TodoItem> repository) {
            _repository = repository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems() {
            return await Task.Factory.StartNew<IEnumerable<TodoItem>>(
                () => _repository.GetItems()
                );
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem?>> GetTodoItem(long id) {
            var itemResult = await Task.Factory.StartNew<ActionResult<TodoItem?>>(
                () => _repository.GetItem(id)
                );

            if (itemResult.Value != null)
                return itemResult;

            return NotFound();
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item) {
            if (id != item.Id)
                return BadRequest();

            await Task.Factory.StartNew(
                () => _repository.Update(item)
                );

            try {
                await Task.Factory.StartNew(
                () => _repository.Save()
                );
            }
            catch (DbUpdateConcurrencyException) {
                var itemFound = await Task.Factory.StartNew<TodoItem?>(
                    () => _repository.GetItem(id)
                    );
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

            await Task.Factory.StartNew(
                () => _repository.Save()
                );

            return CreatedAtAction("GetTodoItem", new { id = item.Id }, item);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {
            var itemFound = await Task.Factory.StartNew<TodoItem?>(
                    () => _repository.GetItem(id)
                    );

            if (itemFound == null)
                return NotFound();

            await Task.Factory.StartNew(
                    () => _repository.Delete(id)
                    );
            
            await Task.Factory.StartNew(
                    () => _repository.Save()
                    );

            return Ok();
        }
    }
}
