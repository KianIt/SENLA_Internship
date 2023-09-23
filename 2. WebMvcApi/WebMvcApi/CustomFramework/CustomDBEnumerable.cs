namespace WebMvcApi.CustomFramework {
    public class CustomDBEnumerable {
        public CustomDBEnumerable() {}
        public CustomDBEnumerable Select(string fields) {
            return this;
        }
        public CustomDBEnumerable Where(string condition) {
            return this;
        }
        public void Insert(string fields, string values) {}
        public void Delete(string condition) {}
        public void Update(string changes, string condition) { }
    }
}
