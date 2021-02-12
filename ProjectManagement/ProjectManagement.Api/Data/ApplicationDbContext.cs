using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Name = "Test Data",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    Priority = Priority.Low
                }
                );
        }
    }
}
