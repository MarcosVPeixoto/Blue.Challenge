using Blue.Challenge.Business.Responses;
using MediatR;

namespace Blue.Challenge.Business.Commands
{
    public class DeleteContactCommand: IRequest<RequestHandlerResponse>
    {
        public int Id { get; set; }
    }
}
