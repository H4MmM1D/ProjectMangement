using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = ProjectManagement.Api.Business.Task;

namespace ProjectManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().Property(p => p.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<Task>().Property(t => t.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("UniqueIdentifier");

            modelBuilder.Entity<Task>().Property(t => t.ProjectId).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<Task>().Property(t => t.Assigny).HasColumnType("UniqueIdentifier");

            modelBuilder.Entity<Task>()
          .HasOne<Project>(t => t.Project)
          .WithMany(p => p.Tasks)
          .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Task>()
         .HasOne<User>(t => t.User)
         .WithMany(u => u.Tasks)
         .HasForeignKey(t => t.Assigny);

            //This is for seeding initial data
            //modelBuilder.Entity<Project>().HasData(
            //    new Project
            //    {
            //        Id = 1,
            //        Name = "Test Data",
            //        StartDate = DateTime.Now,
            //        EndDate = DateTime.Now.AddDays(30),
            //        Priority = Priority.Low
            //    }
            //    );
        }
    }
}
