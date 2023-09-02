using FluentValidation;
using TaskManager.API.Model.Dtos.Note;

namespace TaskManager.API.Validation
{
    public class NoteUpdateValidator : AbstractValidator<NoteUpdate>
    {
        public NoteUpdateValidator()
        {
            RuleFor(noteUpdate => noteUpdate.Notes).NotEmpty().MaximumLength(500);
        }
    }
}
