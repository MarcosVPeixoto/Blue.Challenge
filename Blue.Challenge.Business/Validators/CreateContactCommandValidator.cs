using Blue.Challenge.Business.Commands;
using FluentValidation;
using FluentValidation.Validators;

namespace Blue.Challenge.Business.Validators
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome não pode ser nulo");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email não pode ser nulo")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Formato de email inválido");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Telefone não pode ser nulo")
                .Must(x => x.All(x => char.IsDigit(x)))
                .WithMessage("Apenas números no telefone");

        }
    }
}
