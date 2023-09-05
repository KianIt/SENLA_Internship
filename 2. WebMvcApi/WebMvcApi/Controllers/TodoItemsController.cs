using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMvcApi.Interfaces;
using WebMvcApi.Models;

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
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {
            if (_repository.CheckNull())
                return NotFound();
            
            return await _repository.GetItemsAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem?>> GetTodoItem(long id) {
            if (_repository.CheckNull())
                return NotFound();
   
            var todoItem = await _repository.GetItemAsync(id);

            if (todoItem.Value == null)
                return NotFound();

            return todoItem;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item) {
            if (id != item.Id)
                return BadRequest();

            _repository.Update(item);

            try {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (_repository.GetItemAsync(id).Result == null)
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item) {
            if (_repository.CheckNull())
                return Problem("Entity set 'TodoRepository.TodoContext.TodoItems' is null.");

            _repository.Add(item);
            await _repository.SaveAsync();

            return CreatedAtAction("GetTodoItem", new { id = item.Id }, item);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {
            if (_repository.CheckNull())
                return NotFound();

            var item = await _repository.GetItemAsync(id);
            if (item.Value == null)
                return NotFound();

            _repository.Delete(id);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
