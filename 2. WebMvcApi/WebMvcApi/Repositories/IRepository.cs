namespace WebMvcApi.Repositories {
    // Repository pattern interface
    public interface IRepository<T> : IDisposable
        where T : class {

        public IEnumerable<T> GetItems();

        public T? GetItem(long id);

        public void Add(T item);

        public void Update(T item);

        public void Delete(long id);

        public void Save();
    }
}
