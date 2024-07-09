using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Responses;
using Blue.Challenge.Business.Validators;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using MediatR;
using System.Net;


namespace Blue.Challenge.Business.Handlers.Commands
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, RequestHandlerResponse>
    {

        private readonly IUserRepository _userRepository = userRepository;

        public async Task<RequestHandlerResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                return new RequestHandlerResponse(validation.Errors, HttpStatusCode.BadRequest);
            var dbUser = await _userRepository.GetByEmail(request.Email);
            if (dbUser is not null)
            {
                return new RequestHandlerResponse("Email em uso", HttpStatusCode.BadRequest);
            }
            var user = new User(request.Name, request.Email, PasswordHasher.HashPassword(request.Password));
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return new RequestHandlerResponse(user.UserId, HttpStatusCode.OK);
        }
    }
}
