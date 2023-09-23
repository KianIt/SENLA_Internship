using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;

namespace WebMvcApi.DBContexts {
    public class TodoContext : DbContext {
        public TodoContext(DbContextOptions<TodoContext> options) : 
            base (options) {}

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
