namespace WebMvcApi.Models {
    // Item in TODO list
    public class TodoItem {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
