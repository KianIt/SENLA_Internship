using MediatR;

using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Contracts.Requests {
    public record GetUsersRequest : IRequest<IEnumerable<User>>;

    public class GetUsersHandler : IRequestHandler<GetUsersRequest, IEnumerable<User>> {
        private readonly IRepository<User> _repository;

        public GetUsersHandler(IRepository<User> repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersRequest request, CancellationToken cancellationToken) {
            return await _repository.GetItemsAsync();
        }
    }
}
