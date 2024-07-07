using Blue.Challenge.Business.Responses.Queries;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class CreateContactCommand(string name, string email, string phone) :IRequest<GetContactResponse>
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Phone { get; set; } = phone;
    }
}
