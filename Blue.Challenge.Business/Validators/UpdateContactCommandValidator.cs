using Blue.Challenge.Business.Commands;
using FluentValidation;
using FluentValidation.Validators;

namespace Blue.Challenge.Business.Validators
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator() 
        {
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("Nome não pode ser nulo")
               .MaximumLength(50)
               .WithMessage("Limite máximo de 50 caracteres ultrapassado");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email não pode ser nulo")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Formato de email inválido")
                .MaximumLength(50)
                .WithMessage("Limite máximo de 50 caracteres ultrapassado");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Telefone não pode ser nulo")
                .Must(x => x.All(x => char.IsDigit(x)))
                .WithMessage("Apenas números no telefone")
                .MaximumLength(11)
                .WithMessage("Limite máximo de 11 caracteres ultrapassado");
        }
    }
}
