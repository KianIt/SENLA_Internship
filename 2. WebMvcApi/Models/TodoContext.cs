﻿using Microsoft.EntityFrameworkCore;

namespace _2._WebMvcApi.Models {
    public class TodoContext : DbContext {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
            {}
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
