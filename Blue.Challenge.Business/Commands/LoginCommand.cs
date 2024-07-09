using Blue.Challenge.Business.Responses;
using Blue.Challenge.Business.Responses.Commands;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public record LoginCommand : IRequest<RequestHandlerResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; } 
    }
}
