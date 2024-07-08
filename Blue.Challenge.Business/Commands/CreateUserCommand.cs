using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public record CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
    }

    
}
