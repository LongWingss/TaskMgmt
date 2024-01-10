using System.Threading.Tasks.Dataflow;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.CustomExceptions;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectTaskService(
            IProjectTaskRepository projectTaskRepo,
            IUserRepository userRepo,
            IGroupRepository groupRepo,
            IProjectRepository projectRepo)
        {
            _projectTaskRepository = projectTaskRepo;
            _userRepository = userRepo;
            _groupRepository = groupRepo;
            _projectRepository = projectRepo;
        }

        public async Task<ProjectTaskDto?> Get(int taskId)
        {
            var currentTask = await _projectTaskRepository.GetById(taskId);
            if (currentTask == null)
            {
                return null;
            }

            var projectTaskDTo = new ProjectTaskDto
            {
                TaskId = currentTask.ProjectTaskId,
                Description = currentTask.Description,
                DueDate = currentTask.DueDate.ToShortTimeString(),
                Assignee = currentTask.Assignee.Email,
                CreatedBy = currentTask.Creator.Email,
                CurrentStatus = currentTask.CurrentStatus.StatusText
            };
            return projectTaskDTo;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAll(int groupId, int projectId)
        {
            var allProjectTasks = await _projectTaskRepository.GetAll();
            var currProjectTasks = allProjectTasks
                        .Where(a => a.ProjectId == projectId && a.Project.GroupId == groupId)
                        .ToList();
            var results = new List<ProjectTaskDto>();

            foreach(var task in  currProjectTasks)
            {
                results.Add(new ProjectTaskDto
                {
                    TaskId = task.ProjectTaskId,
                    Description = task.Description,
                    DueDate = task.DueDate.ToString(),
                    Assignee = task.Assignee.Email,
                    CreatedBy = task.Creator.Email,
                    CurrentStatus = task.CurrentStatus.StatusText

                });
            }
            return results;
        }

        public async Task<ProjectTaskDto> CreateTask(int userId, int groupId, int projectId, NewTaskDto newTask)
        {
            var project = await _projectRepository.GetByIdAsync(groupId, projectId);
            if (project is null)
            {
                throw new ProjectNotFoundException("Project not found");
            }

            var assignee = await _userRepository.GetByEmail(newTask.AssigneeMail);
            if (assignee is null)
            {
                throw new AssigneeNotFoundException("Assignee mail is not within group");
            }

            var task = new ProjectTask
            {
                Description = newTask.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = newTask.DueDate,
                CreatorId = userId,
                AssigneeId = assignee.UserId,
                ProjectId = projectId,
                CurrentStatusId = newTask.CurrentStatusId
            };
            await _projectTaskRepository.Add(task);

            return new ProjectTaskDto
            {
                TaskId = task.ProjectTaskId,
                Description = task.Description,
                DueDate = task.DueDate.ToShortDateString(),
                Assignee = task.Assignee.Email,
                CreatedBy = task.Creator.Email,
                CurrentStatus = task.CurrentStatus.StatusText,
            };
        }
    }
}
