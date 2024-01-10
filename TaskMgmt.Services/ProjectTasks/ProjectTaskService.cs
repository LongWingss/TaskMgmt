﻿using System.Threading.Tasks.Dataflow;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.Interfaces;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectTaskService(
            INotificationService notificationService,
            IProjectTaskRepository projectTaskRepo,
            IUserRepository userRepo,
            IProjectRepository projectRepo)
        {
            _notificationService = notificationService;
            _projectTaskRepository = projectTaskRepo;
            _userRepository = userRepo;
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
            await _notificationService.NotifyAsync(task.Assignee.Email, "New Task Assigned", task.Description);
            var created = await _projectTaskRepository.GetById(task.ProjectTaskId);

            return new ProjectTaskDto
            {
                TaskId = task.ProjectTaskId,
                Description = task.Description,
                DueDate = task.DueDate.ToShortDateString(),
                Assignee = task.Assignee.Email,
                CreatedBy = task.Creator.Email,
                CurrentStatus = created?.CurrentStatus?.StatusText ?? "Unable to retrieve status",
            };
        }
    }
}
