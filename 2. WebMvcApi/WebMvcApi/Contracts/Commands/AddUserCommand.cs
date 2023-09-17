using MediatR;

using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Contracts.Commands {
    public record AddUserCommand : IRequest {
        public User User {  get; set; }
        public AddUserCommand(User user) {
            User = user;
        }
    }

    public class AddUserHandler : IRequestHandler<AddUserCommand> {
        private readonly IRepository<User> _repository;

        public AddUserHandler(IRepository<User> repository) {
            _repository = repository;
        }

        public async Task Handle(AddUserCommand request, CancellationToken cancellationToken) {
            await _repository.AddAsync(request.User);
        }
    }
}
