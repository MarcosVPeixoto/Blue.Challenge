using Blue.Challenge.Business.Commands;
using FluentValidation;

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
                .Must(x => x.Contains("@"))
                .WithMessage("Email deve possuir @");

            RuleFor(x => x.Phone)
                .Must(x => x.All(x => char.IsDigit(x)))
                .WithMessage("Apenas números no telefone");

        }
    }
}
