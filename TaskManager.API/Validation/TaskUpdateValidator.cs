using FluentValidation;
using System.Text.RegularExpressions;
using TaskManager.API.Model.Dtos.Task;

namespace TaskManager.API.Validation;

public class TaskUpdateValidator : AbstractValidator<TaskUpdate>
{
    public TaskUpdateValidator()
    {
        RuleFor(taskUpdate => taskUpdate.Name).NotEmpty().MaximumLength(100);
        RuleFor(taskCreate => taskCreate.DeadLine).NotEmpty();
    }
}
