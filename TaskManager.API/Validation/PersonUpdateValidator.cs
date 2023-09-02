using FluentValidation;
using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Validation
{
    public class PersonUpdateValidator : AbstractValidator<PersonUpdate>
    {
        public PersonUpdateValidator()
        {
            RuleFor(personUpdate => personUpdate.Name).NotEmpty().MaximumLength(100);
        }
    }
}
