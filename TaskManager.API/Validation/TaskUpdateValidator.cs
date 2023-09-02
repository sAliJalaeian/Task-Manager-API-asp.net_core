using FluentValidation;
using System.Text.RegularExpressions;
using TaskManager.API.Model.Dtos.Task;

namespace TaskManager.API.Validation;

public class TaskUpdateValidator : AbstractValidator<TaskUpdate>
{
    public TaskUpdateValidator()
    {
        RuleFor(taskUpdate => taskUpdate.Name).NotEmpty().MaximumLength(100);
        RuleFor(taskUpdate => taskUpdate.DeadLine).NotEmpty().MaximumLength(15).Must(hasValidDeadLine);
    }

    private static bool hasValidDeadLine(string deadLine)
    {
        var symbol = new Regex("/");
        var digit = new Regex("(\\d)+");
        return digit.IsMatch(deadLine) && symbol.IsMatch(deadLine);
    }
}
