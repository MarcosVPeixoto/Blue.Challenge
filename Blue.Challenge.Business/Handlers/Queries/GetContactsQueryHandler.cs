using AutoMapper;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Queries;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Infra.Interfaces;
using MediatR;

namespace Blue.Challenge.Business.Handlers.Queries
{
    public class GetContactsQueryHandler(IIdentityService identityService, 
                                        IContactRepository contactRepository,
                                        IMapper mapper) 
                                        : IRequestHandler<GetContactsQuery, List<GetContactResponse>>
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetContactResponse>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserId();
            var contacts = await _contactRepository.GetByUserIdAsync(userId);
            return _mapper.Map<List<GetContactResponse>>(contacts);
        }
    }
}
