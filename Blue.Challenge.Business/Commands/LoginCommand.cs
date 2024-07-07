using Blue.Challenge.Business.Responses.Commands;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public record LoginCommand(string email, string password) : IRequest<LoginCommandResponse>
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
