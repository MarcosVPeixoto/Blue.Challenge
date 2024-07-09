using Blue.Challenge.Business.Responses;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public record CreateUserCommand : IRequest<RequestHandlerResponse>
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
    }

    
}
