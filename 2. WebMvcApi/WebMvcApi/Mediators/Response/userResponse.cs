namespace WebMvcApi.Mediators.Response {
    public class UserResponse<T> : IResponse{
        public T Value { get; set; }
        public UserResponse(T value) {
            Value = value;
        }   
    }
}

