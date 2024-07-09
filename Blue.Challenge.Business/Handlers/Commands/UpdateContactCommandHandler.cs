using AutoMapper;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Responses;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Infra.Interfaces;
using MediatR;
using System.Net;

namespace Blue.Challenge.Business.Handlers.Commands
{
    public class UpdateContactCommandHandler(IMapper mapper, IContactRepository repository, IIdentityService identityService, IUserRepository userRepository) : IRequestHandler<UpdateContactCommand, RequestHandlerResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IContactRepository _contactRepository = repository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IIdentityService _identityService = identityService;

        public async Task<RequestHandlerResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
                return new RequestHandlerResponse("Contato não encontrado", HttpStatusCode.BadRequest);
            var userId = _identityService.GetUserId();
            var user = await _userRepository.GetByUserId(userId);
            if (user.Id != contact.UserId)
                return new RequestHandlerResponse("Contato não pertence ao usuário", HttpStatusCode.BadRequest);
            contact.UpdateFields(request.Name, request.Email, request.Phone);
            _contactRepository.Update(contact);
            await _contactRepository.SaveChangesAsync();
            return new RequestHandlerResponse(_mapper.Map<GetContactQueryResponse>(contact), HttpStatusCode.OK);
        }
    }
}
