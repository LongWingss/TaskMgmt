using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskMgmt.DataAccess.Models
{
    public class TaskMgmntContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<Invitation> Invitations { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
        public DbSet<TaskStatus> TaskStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=127.0.0.1,1405;
                Database=TaskMgmnt;
                User Id=SA;
                Password=Sql@2022!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasKey(e => new { e.UserId, e.GroupId });

            modelBuilder.Entity<Invitation>().HasOne(e => e.InvitedByUserNavigation)
                .WithMany(e => e.Invitations)
                .HasPrincipalKey(e => e.UserId)
                .HasForeignKey(e => e.InvitedByUser)
                .IsRequired();

            modelBuilder.Entity<Project>().HasOne(e => e.Owner)
                .WithMany(e => e.Projects)
                .HasPrincipalKey(e => e.UserId)
                .HasForeignKey(e => e.OwnerId)
                .IsRequired();

            modelBuilder.Entity<TaskStatus>().HasIndex(e => new { e.ProjectId, e.StatusText })
                .IsUnique();

            modelBuilder.Entity<ProjectTask>(
                nestedBuilder => {
                    nestedBuilder.HasOne(e => e.Assignee)
                        .WithMany()
                        .HasPrincipalKey(e => e.UserId)
                        .HasForeignKey(e => e.AssigneeId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    nestedBuilder.HasOne(e => e.Creator)
                        .WithMany()
                        .HasPrincipalKey(e => e.UserId)
                        .HasForeignKey(e => e.CreatorId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
            
            modelBuilder.Entity<TaskStatus>().HasOne(e => e.Project)
                    .WithMany()
                    .HasPrincipalKey(e => e.ProjectId)
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
        }
    }
}
