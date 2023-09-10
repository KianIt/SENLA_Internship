using WebMvcApi.DBContexts;
using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;
using WebMvcApi.Models;

namespace WebMvcApi.Mediators.Handlers {
    public class AddUserHandler : AbstractHandler {
        public AddUserHandler(UserContext context) : base(context) {
        }

        public override IResponse Respond(IRequest request) {
            _context.Add(((AddUserRequest) request).user);
            _context.SaveChanges();
            return new UserResponse<object?>(null);
        }
    }
}
