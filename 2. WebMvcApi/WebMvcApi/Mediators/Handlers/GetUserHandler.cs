using WebMvcApi.DBContexts;
using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;
using WebMvcApi.Models;

namespace WebMvcApi.Mediators.Handlers {
    public class GetUserHandler : AbstractHandler {
        public GetUserHandler(UserContext context) : base(context) {
        }

        public override IResponse Respond(IRequest request) {
            return new UserResponse<User?>(_context.Users.Find(
                ((GetUserRequest) request).id));
        }
    }
}
