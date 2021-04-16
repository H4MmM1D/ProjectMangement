using AutoMapper;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos;

namespace ProjectManagement.Api.Mapper
{
    public class Mappings : Profile
    {
        public Mappings() 
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, EditProjectDto>().ReverseMap();

            CreateMap<Task, TaskDto>().ReverseMap();
            CreateMap<Task, CreateTaskDto>().ReverseMap();
            CreateMap<Task, EditTaskDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, EditUserDto>().ReverseMap();
        }
    }
}
