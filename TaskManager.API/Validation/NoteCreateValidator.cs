using FluentValidation;
using TaskManager.API.Model.Dtos.Note;

namespace TaskManager.API.Validation
{
    public class NoteCreateValidator : AbstractValidator<NoteCreate>
    {
        public NoteCreateValidator()
        {
            RuleFor(noteCreate => noteCreate.Notes).NotEmpty().MaximumLength(500);
        }
    }
}
