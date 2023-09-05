using Microsoft.EntityFrameworkCore;

namespace WebMvcApi.Models {
    public class TodoContext : DbContext {
        public TodoContext(DbContextOptions<TodoContext> options) : 
            base (options) {}

        // Automatically filled set of items
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
