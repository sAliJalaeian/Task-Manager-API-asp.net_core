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
        CreateMap<TaskCreate, Task>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<TaskUpdate, Task>();
        CreateMap<Task, TaskGet>();

        CreateMap<NoteCreate, Note>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<NoteUpdate, Note>();
        CreateMap<Note, NoteGet>();

        CreateMap<PersonCreate, Person>()
            .ForMember(dest => dest.Tasks, opt => opt.Ignore())
            .ForMember(dest => dest.Notebook, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<PersonUpdate, Person>()
            .ForMember(dest => dest.Tasks, opt => opt.Ignore())
            .ForMember(dest => dest.Notebook, opt => opt.Ignore());
        CreateMap<Person, PersonDetails>();
        /*.ForMember(dest => dest.Tasks, opt => opt.Ignore())
        .ForMember(dest => dest.Notebook, opt => opt.Ignore())
        .ForMember(dest => dest.Id, opt => opt.Ignore());*/
        CreateMap<Person, PersonList>();
    }
}
