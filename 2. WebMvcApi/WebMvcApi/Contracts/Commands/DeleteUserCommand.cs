using MediatR;
using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Contracts.Commands {
    public record DeleteUserCommand : IRequest {
        public int Id { get; set; }

        public DeleteUserCommand(int id) {
            Id = id;
        }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand> {
        private readonly IRepository<User> _repository;

        public DeleteUserHandler(IRepository<User> repository) {
            _repository = repository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
            await _repository.DeleteAsync(request.Id);
        }
    }
}
