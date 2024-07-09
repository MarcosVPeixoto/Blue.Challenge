using Blue.Challenge.Business.Commands;
using FluentValidation;
using FluentValidation.Validators;

namespace Blue.Challenge.Business.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email não pode ser nulo")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Formato de email inválido");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Senha não pode ser nula");
        }
    }
}
