using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskManager.API.Exceptions;

namespace TaskManager.API;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (PersonNotFoundException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Person for id {ex.Id} not found.",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (NoteNotFoundException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Note for id {ex.Id} not found.",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (NotesNotFoundException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Notes {JsonSerializer.Serialize(ex.NoteIds)} not found.",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (TaskNotFoundException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Task for id {ex.Id} not found.",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (TasksNotFoundException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Tasks {JsonSerializer.Serialize(ex.TaskIds)} not found.",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = JsonSerializer.Serialize(ex.Errors),
                Instance = "",
                Title = "Validation Error",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetail = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = "",
                Title = "Internal Server Error - something went wrong",
                Type = "Error"
            };
            
            var problemDetailsJson = JsonSerializer.Serialize(problemDetail);
            await context.Response.WriteAsync(problemDetailsJson);
        }
    }
}
