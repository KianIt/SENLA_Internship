using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.DBContexts;

namespace WebMvcApi.Repositories {
    // Repository pattern implementation
    public class TodoRepository : IRepository<TodoItem> {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context) {
            _context = context;
        }

        public IEnumerable<TodoItem> GetItems() {
            return _context.TodoItems.ToList();
        }

        public TodoItem? GetItem(long id) {
            return _context.TodoItems.Find(id); ;
        }

        public void Add(TodoItem item) {
            _context.Add(item);
        }

        public void Update(TodoItem item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(long id) {
            var item = GetItem(id);
            if (item != null)
                _context.TodoItems.Remove(item);
        }

        public void Save() {
            _context.SaveChangesAsync();
        }

        private bool disposed = false;
        public void Dispose() {
            if (!disposed) {
                _context.Dispose();
            }
            disposed = true;
        }
    }
}
