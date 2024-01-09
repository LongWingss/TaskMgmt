using AutoMapper;
using TaskMgmt.DataAccess.Dtos;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Profiles
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
