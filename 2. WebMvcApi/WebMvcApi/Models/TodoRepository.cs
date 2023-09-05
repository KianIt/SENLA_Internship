using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMvcApi.Interfaces;

namespace WebMvcApi.Models {
    // TodoItem repository implementation
    public class TodoRepository : IRepository<TodoItem> {
        // data storage context
        private readonly TodoContext _context;

        // context from AddDbContext
        public TodoRepository(TodoContext context) {
            _context = context; 
        }

        // checks if context is null
        public bool CheckNull() {
            return _context == null;
        }

        // returns all items asyncly
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItemsAsync() {
            return await _context.TodoItems.ToListAsync(); 
        }

        // returns item by id asyncly
        public async Task<ActionResult<TodoItem?>> GetItemAsync(long id) {
            return await _context.TodoItems.FindAsync(id);
        }

        // inserts item
        public void Add(TodoItem item) {
            _context.Add(item);
        }

        // updates item by id
        public void Update(TodoItem item) {
            _context.Entry(item).State = EntityState.Modified; 
        }

        // removes item by id
        public async void Delete(long id) {
            var item = await GetItemAsync(id);
            if (item.Value != null)
                _context.TodoItems.Remove(item.Value);
        }

        // saves changes asyncly
        public async Task<int> SaveAsync() {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        // disposes the object
        public void Dispose() {
            if (!disposed) {
                _context.Dispose();
            }
            disposed = true;
        }
    }
}
