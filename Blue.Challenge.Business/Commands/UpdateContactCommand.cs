using Blue.Challenge.Business.Responses.Queries;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class UpdateContactCommand : IRequest<GetContactResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
