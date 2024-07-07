using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Validators;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using FluentValidation;
using MediatR;


namespace Blue.Challenge.Business.Handlers.Commands
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
    {

        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);
            var dbUser = await _userRepository.GetByEmail(request.Email);
            if (dbUser is not null)
            {
                throw new Exception("Email em uso");
            }
            var user = new User(request.Name, request.Email, PasswordHasher.HashPassword(request.Password));
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user.UserId;

        }
    }
}
