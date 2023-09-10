using WebMvcApi.Mediators.Handlers;
using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;

namespace WebMvcApi.Mediators {
    public class UserMediator : IMediator {
        private readonly GetUsersHandler _getUsersHandler;
        private readonly GetUserHandler _getUserHandler;
        private readonly AddUserHandler _addUserHandler;


        public UserMediator(GetUsersHandler getUsersHandler,
                            GetUserHandler getUserHandler,
                            AddUserHandler addUsersHandler) {
            _getUsersHandler = getUsersHandler;
            _getUserHandler = getUserHandler;
            _addUserHandler = addUsersHandler;
        }

        public Task<IResponse> SendAsync(IRequest request) {
            if (request is GetUsersRequest) {
                return Task.Run(() => _getUsersHandler.Respond(request));
            }
            else if (request is GetUserRequest) {
                return Task.Run(() => _getUserHandler.Respond(request));
            }
            else {
                return Task.Run(() => _addUserHandler.Respond(request));
            }
        }
    }
}
