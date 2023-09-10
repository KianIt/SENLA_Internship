using WebMvcApi.Models;

namespace WebMvcApi.Mediators.Requests {
    public class AddUserRequest : IRequest {
        public User user { get; set; }
        public AddUserRequest(User user) {
            this.user = user;
        }
    }
}
