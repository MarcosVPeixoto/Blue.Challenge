using Blue.Challenge.App.Extensions;
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
        public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand command)
        {
            return this.ValidateResponse(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginCommandResponse>> Login(LoginCommand command)
        {
            return this.ValidateResponse(await _mediator.Send(command)); 
        }


    }
}
