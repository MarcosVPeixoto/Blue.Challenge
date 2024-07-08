using AutoMapper;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Infra.Interfaces;
using MediatR;

namespace Blue.Challenge.Business.Handlers
{
    public class UpdateContactCommandHandler(IMapper mapper, IContactRepository repository, IIdentityService identityService, IUserRepository userRepository) : IRequestHandler<UpdateContactCommand, GetContactResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IContactRepository _contactRepository = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IIdentityService _identityService = identityService;

        public async Task<GetContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
                throw new Exception("Contato não encontrado");
            var userId = _identityService.GetUserId();
            var user = await _userRepository.GetByUserId(userId);
            if (user.Id != contact.UserId)
                throw new Exception("Contato não pertence ao usuário");
            contact.UpdateFields(request.Name, request.Email, request.Phone);
            _contactRepository.Update(contact);
            await _contactRepository.SaveChangesAsync();
            return _mapper.Map<GetContactResponse>(contact);
        }
    }
}
