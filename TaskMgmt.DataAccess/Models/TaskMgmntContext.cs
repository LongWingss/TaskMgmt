using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskMgmt.DataAccess.Models
{
    public partial class TaskMgmntContext : DbContext
    {
        public TaskMgmntContext()
        {
        }

        public TaskMgmntContext(DbContextOptions<TaskMgmntContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Invitation> Invitations { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<TaskStatus> TaskStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1,1405;Database=TaskMgmnt;User Id=SA;Password=Sql@2022!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.GroupName, "UQ__Groups__6EFCD4342142682F")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GroupName).HasMaxLength(255);
            });

            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.HasIndex(e => e.Token, "UQ__Invitati__1EB4F8179088204C")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InviteeEmail).HasMaxLength(255);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Invitatio__Group__44FF419A");

                entity.HasOne(d => d.InvitedByUserNavigation)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.InvitedByUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invitatio__Invit__45F365D3");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectDescription).HasMaxLength(1000);

                entity.Property(e => e.ProjectName).HasMaxLength(255);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Projects__GroupI__4AB81AF0");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projects__OwnerI__4BAC3F29");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053435DEC9D4")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(60);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId })
                    .HasName("PK__UserGrou__A6C1637A8074B070");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserGroup__Group__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserGroup__UserI__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
