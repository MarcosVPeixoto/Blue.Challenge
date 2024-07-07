using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Infra.Interfaces;
using MediatR;

namespace Blue.Challenge.Business.Handlers.Commands
{
    public class DeleteContactCommandHandler(IContactRepository contactRepository,                                             
                                             IIdentityService identityService) : IRequestHandler<DeleteContactCommand, int>
    {
        private readonly IContactRepository _contactRepository = contactRepository;        
        private readonly IIdentityService _identityService = identityService;

        public async Task<int> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
                throw new Exception("Contato não encontrado");
            var belongsLoggedUser = contact.User.UserId == _identityService.GetUserId();
            if (!belongsLoggedUser)
                throw new Exception("Contato não pertence ao usuário");
            _contactRepository.Remove(contact);
            await _contactRepository.SaveChangesAsync();
            return contact.Id;
        }
    }
}
