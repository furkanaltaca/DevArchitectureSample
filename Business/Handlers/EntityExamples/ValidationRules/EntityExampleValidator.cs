
using Business.Handlers.EntityExamples.Commands;
using FluentValidation;

namespace Business.Handlers.EntityExamples.ValidationRules
{

    public class CreateEntityExampleValidator : AbstractValidator<CreateEntityExampleCommand>
    {
        public CreateEntityExampleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
    public class UpdateEntityExampleValidator : AbstractValidator<UpdateEntityExampleCommand>
    {
        public UpdateEntityExampleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}