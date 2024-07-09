using AutoMapper;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Interfaces;
using Blue.Challenge.Business.Responses;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Business.Validators;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using FluentValidation;
using MediatR;
using System.Net;


namespace Blue.Challenge.Business.Handlers.Commands
{
    public class CreateContactCommandHandler(IContactRepository contactRepository, 
                                             IUserRepository userRepository, 
                                             IIdentityService identityService,
                                             IMapper mapper) : IRequestHandler<CreateContactCommand, RequestHandlerResponse>
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IIdentityService _identityService = identityService;
        private readonly IMapper _mapper = mapper;

        public async Task<RequestHandlerResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateContactCommandValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return new RequestHandlerResponse(validation.Errors, HttpStatusCode.BadRequest);
            var userId = _identityService.GetUserId();
            var user = await _userRepository.GetByUserId(userId);
            var contact = new Contact(request.Name, request.Email, request.Phone, user.Id) ;            
            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveChangesAsync();
            var contactResponse = _mapper.Map<GetContactQueryResponse>(contact);
            return new RequestHandlerResponse(contactResponse, HttpStatusCode.OK);
        }
    }
}
