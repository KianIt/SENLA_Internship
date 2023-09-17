using MediatR;
using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Contracts.Requests {
    public record GetUserByIdRequest : IRequest<User?> {
        public int Id { get; set; }
        public GetUserByIdRequest(int id) { 
            Id = id;
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, User?> {
        private readonly IRepository<User> _repository;

        public GetUserByIdHandler(IRepository<User> repository) {
            _repository = repository;
        }

        public async Task<User?> Handle(GetUserByIdRequest request, CancellationToken cancellationToken) {
            return await _repository.GetItemAsync(request.Id);
        }
    }
}