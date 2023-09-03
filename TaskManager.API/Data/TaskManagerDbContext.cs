using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Model.Domain;
using Task = TaskManager.API.Model.Domain.Task;

namespace TaskManager.API.Data;

public class TaskManagerDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Note> Notes { get; set; }

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=TaskManager.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasKey(e => e.Id);
        modelBuilder.Entity<Task>().HasKey(e => e.Id);
        modelBuilder.Entity<Note>().HasKey(e => e.Id);

        modelBuilder.Entity<Person>().HasMany(e => e.Tasks).WithOne(e => e.PersonTaken).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Person>().HasMany(e => e.Notebook).WithOne(e => e.PersonTaken).OnDelete(DeleteBehavior.NoAction);
        

        base.OnModelCreating(modelBuilder);
    }
}
