using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Queries;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Infra.Interfaces;
using MediatR;

namespace Blue.Challenge.Business.Handlers.Commands
{
    public class DeleteContactCommandHandler(IContactRepository contactRepository,                                             
                                             IIdentityService identityService,
                                             IMediator mediator) : IRequestHandler<DeleteContactCommand, List<GetContactResponse>>
    {
        private readonly IContactRepository _contactRepository = contactRepository;        
        private readonly IIdentityService _identityService = identityService;
        private readonly IMediator _mediator = mediator;

        public async Task<List<GetContactResponse>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
                throw new Exception("Contato não encontrado");
            var belongsLoggedUser = contact.User.UserId == _identityService.GetUserId();
            if (!belongsLoggedUser)
                throw new Exception("Contato não pertence ao usuário");
            _contactRepository.Remove(contact);
            await _contactRepository.SaveChangesAsync();
            return await _mediator.Send(new GetContactsQuery());
        }
    }
}
