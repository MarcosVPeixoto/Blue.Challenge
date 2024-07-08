using Blue.Challenge.Business.Responses.Commands;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public record LoginCommand : IRequest<LoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; } 
    }
}
