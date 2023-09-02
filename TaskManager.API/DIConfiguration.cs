using TaskManager.API.Profiles;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Services;
using TaskManager.API.Validation;

namespace TaskManager.API;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IPersonService, PersonService>();

        services.AddScoped<PersonCreateValidator>();
        services.AddScoped<PersonUpdateValidator>();
        services.AddScoped<TaskCreateValidator>();
        services.AddScoped<TaskUpdateValidator>();
        services.AddScoped<NoteCreateValidator>();
        services.AddScoped<NoteUpdateValidator>();
    }
}
