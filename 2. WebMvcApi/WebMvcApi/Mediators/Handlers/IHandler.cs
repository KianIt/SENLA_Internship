using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;

namespace WebMvcApi.Mediators.Handlers {
    public interface IHandler {
        public IResponse Respond(IRequest request);
    }
}
