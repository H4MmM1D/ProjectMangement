using AutoMapper;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos.Meeting;
using ProjectManagement.Api.Business.Dtos.Member;
using ProjectManagement.Api.Business.Dtos.Project;
using ProjectManagement.Api.Business.Dtos.Task;
using ProjectManagement.Api.Business.Dtos.User;

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

            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<Member, CreateMemberDto>().ReverseMap();
            CreateMap<Member, EditMemberDto>().ReverseMap();

            CreateMap<Meeting, MeetingDto>().ReverseMap();
            CreateMap<Meeting, CreateMeetingDto>().ReverseMap();
            CreateMap<Meeting, EditMeetingDto>().ReverseMap();
        }
    }
}
