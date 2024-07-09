using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Queries;
using Blue.Challenge.Business.Responses;
using Blue.Challenge.Infra.Interfaces;
using MediatR;
using System.Net;

namespace Blue.Challenge.Business.Handlers.Commands
{
    public class DeleteContactCommandHandler(IContactRepository contactRepository,                                             
                                             IIdentityService identityService,
                                             IMediator mediator) : IRequestHandler<DeleteContactCommand, RequestHandlerResponse>
    {
        private readonly IContactRepository _contactRepository = contactRepository;        
        private readonly IIdentityService _identityService = identityService;
        private readonly IMediator _mediator = mediator;

        public async Task<RequestHandlerResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
                return new RequestHandlerResponse("Contato não encontrado", HttpStatusCode.BadRequest);
            var belongsLoggedUser = contact.User.UserId == _identityService.GetUserId();
            if (!belongsLoggedUser)
                return new RequestHandlerResponse("Contato não pertence ao usuário", HttpStatusCode.BadRequest);
            _contactRepository.Remove(contact);
            await _contactRepository.SaveChangesAsync();
            var contacts = await _mediator.Send(new GetContactsQuery());
            return new RequestHandlerResponse(contacts, HttpStatusCode.OK);
        }
    }
}
