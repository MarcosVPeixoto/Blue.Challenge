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

        }
    }
}
