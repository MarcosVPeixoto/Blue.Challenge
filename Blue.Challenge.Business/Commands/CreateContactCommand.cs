using Blue.Challenge.Business.Responses;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class CreateContactCommand(string name, string email, string phone) :IRequest<RequestHandlerResponse>
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Phone { get; set; } = phone;
    }
}
