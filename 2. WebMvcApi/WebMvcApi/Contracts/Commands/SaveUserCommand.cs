using MediatR;
using WebMvcApi.Models;
using WebMvcApi.Repositories;

namespace WebMvcApi.Contracts.Commands {
    public record SaveUserCommand : IRequest;

    public class SaveUserhandler : IRequestHandler<SaveUserCommand> {
        private readonly IRepository<User> _repository;

        public SaveUserhandler(IRepository<User> repository) {
            _repository = repository;
        }

        public async Task Handle(SaveUserCommand request, CancellationToken cancellationToken) {
            await _repository.SaveAsync();
        }
    }
}
