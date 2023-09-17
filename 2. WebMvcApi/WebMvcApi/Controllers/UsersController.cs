using Microsoft.AspNetCore.Mvc;

using MediatR;

using WebMvcApi.Models;
using WebMvcApi.Contracts.Requests;
using WebMvcApi.Contracts.Commands;

namespace WebMvcApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers() {
            var getUsersRequest = new GetUsersRequest();
            var response = await _mediator.Send(getUsersRequest);
            return response;
        }

        [HttpGet("id")]
        public async Task<ActionResult<User?>> GetUser(int id) {
            var getUserByIdRequest = new GetUserByIdRequest(id);
            var response = await _mediator.Send(getUserByIdRequest);

            if (response == null)
                return NotFound();

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user) {
            var addUserCommand = new AddUserCommand(user);
            await _mediator.Send(addUserCommand);

            var saveCommand = new SaveUserCommand();
            await _mediator.Send(saveCommand);

            return Ok();
        }

    }
}
