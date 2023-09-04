using FluentValidation;
using System.Text.RegularExpressions;
using TaskManager.API.Model.Dtos.Task;

namespace TaskManager.API.Validation;

public class TaskCreateValidator : AbstractValidator<TaskCreate>
{
    public TaskCreateValidator()
    {
        RuleFor(taskCreate => taskCreate.Name).NotEmpty().MaximumLength(100);
        RuleFor(taskCreate => taskCreate.DeadLine).NotEmpty();
    }
}
