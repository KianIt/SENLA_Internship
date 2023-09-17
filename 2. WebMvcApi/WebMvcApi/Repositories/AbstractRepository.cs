namespace WebMvcApi.Repositories {
    public abstract class AbstractRepository<T> : IRepository<T>
        where T: class {
        public abstract IEnumerable<T> GetItems();

        public async Task<IEnumerable<T>> GetItemsAsync() {
            return await Task.Factory.StartNew(() => GetItems());
        }

        public abstract T? GetItem(int id);

        public async Task<T?> GetItemAsync(int id) {
            return await Task.Factory.StartNew(() => GetItem(id));
        }

        public abstract void Add(T item);

        public async Task AddAsync(T item) {
            await Task.Factory.StartNew(() => Add(item));
        }

        public abstract void Update(T item);

        public async Task UpdateAsync(T item) {
            await Task.Factory.StartNew(() => Update(item));
        }

        public abstract void Delete(int id);

        public async Task DeleteAsync(int id) {
            await Task.Factory.StartNew(() => Delete(id));
        }

        public abstract void Save();

        public async Task SaveAsync() {
            await Task.Factory.StartNew(() => Save());
        }

    }
}
