using Microsoft.EntityFrameworkCore;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.ProjectTasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskMgmntContext>(
    opts => {
        opts.UseSqlServer(@"Server=127.0.0.1,1405;
                Database=TaskMgmnt;
                User Id=SA;
                Password=Sql@2022!;");
    });

builder.Services.AddScoped<IProjectTaskStatusRepository, ProjectTaskStatusRepository>();
builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddScoped<IProjectTaskStatusService, ProjectTaskStatusService>();
builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
