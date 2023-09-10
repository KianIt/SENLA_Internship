using WebMvcApi.DBContexts;
using WebMvcApi.Models;
using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;

namespace WebMvcApi.Mediators.Handlers {
    public class GetUsersHandler : AbstractHandler {
        public GetUsersHandler(UserContext context) : base(context) {
        }

        public override IResponse Respond(IRequest request) {
            return new UserResponse<List<User>>(_context.Users.ToList());
        }
    }
}
