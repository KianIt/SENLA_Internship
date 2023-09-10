using Microsoft.AspNetCore.Mvc;

using WebMvcApi.Models;
using WebMvcApi.Mediators;
using WebMvcApi.Mediators.Requests;
using WebMvcApi.Mediators.Response;

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
            var response = await _mediator.SendAsync(new GetUsersRequest());

            return ((UserResponse<List<User>>) response).Value;
        }

        [HttpGet("id")]
        public async Task<ActionResult<User?>> GetUser(int id) {
            var response = await _mediator.SendAsync(new GetUserRequest(id));

            if (((UserResponse<User?>) response).Value == null)
                return NotFound();

            return ((UserResponse<User?>) response).Value;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user) {
            await _mediator.SendAsync(new AddUserRequest(user));

            return Ok();
        }

    }
}
