using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class DeleteContactCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }
}
