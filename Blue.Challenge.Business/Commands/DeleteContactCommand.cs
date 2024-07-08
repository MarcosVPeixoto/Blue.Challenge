using Blue.Challenge.Business.Responses.Queries;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class DeleteContactCommand(int id) : IRequest<List<GetContactResponse>>
    {
        public int Id { get; set; } = id;
    }
}
