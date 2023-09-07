using Microsoft.EntityFrameworkCore;
using TaskManager.API;
using TaskManager.API.Data;
using TaskManager.API.Model.Domain;
using TaskManager.API.Repositories;
using TaskManager.API.Repositories.Interface;
using Task = TaskManager.API.Model.Domain.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
//var dbfilename = Environment.GetEnvironmentVariable("DB_FILENAME");
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerConnectionString")));
builder.Services.AddScoped<IGenericRepository<Task>, GenericRepository<Task>>();
builder.Services.AddScoped<IGenericRepository<Note>, GenericRepository<Note>>();
builder.Services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
    //dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
