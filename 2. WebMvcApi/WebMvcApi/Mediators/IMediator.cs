using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;

namespace WebMvcApi.Mediators {
    public interface IMediator {
        public Task<IResponse> SendAsync(IRequest request);
    }
}
