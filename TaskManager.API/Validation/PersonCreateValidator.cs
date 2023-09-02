using FluentValidation;
using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Validation
{
    public class PersonCreateValidator : AbstractValidator<PersonCreate>
    {
        public PersonCreateValidator()
        {
            RuleFor(personCreate => personCreate.Name).NotEmpty().MaximumLength(100);
        }
    }
}
