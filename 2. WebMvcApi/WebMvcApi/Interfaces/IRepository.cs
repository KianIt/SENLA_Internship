using Microsoft.AspNetCore.Mvc;
using WebMvcApi.Models;

namespace WebMvcApi.Interfaces {
    // Repository pattern interface
    public interface IRepository<T> : IDisposable 
        where T: class {

        // checks if context is null
        public bool CheckNull();

        // returns all items asyncly
        public Task<ActionResult<IEnumerable<TodoItem>>> GetItemsAsync();

        // returns item by id asyncly
        public Task<ActionResult<TodoItem?>> GetItemAsync(long id);

        // inserts item
        public void Add(TodoItem item);

        // updates item by id
        public void Update(T item);

        // deletes item by id
        public void Delete(long id);

        // saves changes asyncly
        public Task<int> SaveAsync();
    }
}
