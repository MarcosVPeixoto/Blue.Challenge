using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Responses.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blue.Challenge.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("User")]
        public async Task<Guid> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<LoginCommandResponse> Login(LoginCommand command)
        {
            return await _mediator.Send(command);            
        }


    }
}
