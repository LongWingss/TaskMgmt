using AutoMapper;
using TaskMgmt.Api.Dtos;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Api.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
