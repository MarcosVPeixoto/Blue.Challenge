using Blue.Challenge.Business.Responses;
using Blue.Challenge.Business.Responses.Queries;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class UpdateContactCommand : IRequest<RequestHandlerResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
