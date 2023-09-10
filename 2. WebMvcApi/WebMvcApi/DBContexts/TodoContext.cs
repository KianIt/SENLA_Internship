using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;

namespace WebMvcApi.DBContexts {
    // Context of memory-in database for TodoItems
    public class TodoContext : DbContext {
        public TodoContext(DbContextOptions<TodoContext> options) : 
            base (options) {}

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
