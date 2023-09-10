using Microsoft.EntityFrameworkCore;
using WebMvcApi.Models;

namespace WebMvcApi.DBContexts {
    public class UserContext : DbContext {
        public UserContext(DbContextOptions<UserContext> options) :
            base(options) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
