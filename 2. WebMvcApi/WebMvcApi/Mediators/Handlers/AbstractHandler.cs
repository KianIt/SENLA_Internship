using WebMvcApi.DBContexts;
using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;

namespace WebMvcApi.Mediators.Handlers {
    public abstract class AbstractHandler {
        protected readonly UserContext _context;

        public AbstractHandler(UserContext context) {
            _context = context;
        }

        public abstract IResponse Respond(IRequest request);
    }
}
