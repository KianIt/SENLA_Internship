﻿using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.DBContexts;

namespace WebMvcApi.Repositories {
    public class TodoContextRepository : AbstractRepository<TodoItem> {
        TodoContext _context { get; set; }
        public TodoContextRepository(TodoContext context) {
            _context = context;
        }

        public override IEnumerable<TodoItem> GetItems() {
            return _context.TodoItems.ToList();
        }

        public override TodoItem? GetItem(int id) {
            return _context.TodoItems.Find(id); ;
        }

        public override void Add(TodoItem item) {
            _context.Add(item);
        }

        public override void Update(TodoItem item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public override void Delete(int id) {
            var item = GetItem(id);
            if (item != null)
                _context.TodoItems.Remove(item);
        }

        public override void Save() {
            _context.SaveChangesAsync();
        }
    }
}
