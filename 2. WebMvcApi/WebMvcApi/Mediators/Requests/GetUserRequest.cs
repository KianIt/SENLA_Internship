namespace WebMvcApi.Mediators.Requests {
    public class GetUserRequest : IRequest {
        public int id { get; set; }
        public GetUserRequest(int id) {
            this.id = id;
        }
    }
}
