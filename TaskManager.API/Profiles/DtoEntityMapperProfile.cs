using AutoMapper;
using TaskManager.API.Model.Domain;
using TaskManager.API.Model.Dtos.Note;
using TaskManager.API.Model.Dtos.Person;
using TaskManager.API.Model.Dtos.Task;
using Task = TaskManager.API.Model.Domain.Task;

namespace TaskManager.API.Profiles;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<TaskCreate, Task>();
        CreateMap<TaskUpdate, Task>();
        CreateMap<Task, TaskGet>();
        CreateMap<Task, TaskGetForPerson>();

        CreateMap<NoteCreate, Note>();
        CreateMap<NoteUpdate, Note>();
        CreateMap<Note, NoteGet>();
        CreateMap<Note, NoteGetForPerson>();

        CreateMap<PersonCreate, Person>();
        CreateMap<PersonUpdate, Person>();
        CreateMap<Person, PersonDetails>();
        CreateMap<Person, PersonList>();
        CreateMap<Person, PersonGet>();
    }
}
