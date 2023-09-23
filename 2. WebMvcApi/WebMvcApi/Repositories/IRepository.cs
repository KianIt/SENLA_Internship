namespace WebMvcApi.Repositories {
    public interface IRepository<T>
        where T : class {

        public IEnumerable<T> GetItems();

        public Task<IEnumerable<T>> GetItemsAsync();

        public T? GetItem(int id);
        public Task<T?> GetItemAsync(int id);

        public void Add(T item);

        public Task AddAsync(T item);

        public void Update(T item);

        public Task UpdateAsync(T item);

        public void Delete(int id);

        public Task DeleteAsync(int id);

        public void Save();

        public Task SaveAsync();
    }
}
