using Blue.Challenge.Business.Responses.Queries;
using MediatR;

namespace Blue.Challenge.Business.Queries
{
    public class GetContactsQuery : IRequest<List<GetContactQueryResponse>>
    {

    }
}
