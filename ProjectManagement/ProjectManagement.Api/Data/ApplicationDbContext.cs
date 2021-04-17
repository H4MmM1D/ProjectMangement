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
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PrivilegeLevel> PrivilegeLevels { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPrivilegeLevel> UserPrivilegeLevels { get; set; }
        public DbSet<MemberRole> MemberRoles { get; set; }
        public DbSet<MemberPrivilegeLevel> MemberPrivilegeLevels { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().Property(p => p.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<Task>().Property(t => t.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<Member>().Property(m => m.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<Role>().Property(r => r.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<PrivilegeLevel>().Property(pl => pl.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<UserRole>().Property(ur => ur.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<UserPrivilegeLevel>().Property(upl => upl.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<MemberRole>().Property(ur => ur.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<MemberPrivilegeLevel>().Property(upl => upl.Id).HasColumnType("UniqueIdentifier");
            modelBuilder.Entity<Meeting>().Property(m => m.Id).HasColumnType("UniqueIdentifier");

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

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.RoleId, ur.UserId });

            modelBuilder.Entity<UserRole>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);


            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<UserPrivilegeLevel>().HasKey(upl => new { upl.PrivilegeLevelId, upl.UserId });

            modelBuilder.Entity<UserPrivilegeLevel>()
                .HasOne<User>(upl => upl.User)
                .WithMany(u => u.UserPrivilegeLevels)
                .HasForeignKey(upl => upl.UserId);


            modelBuilder.Entity<UserPrivilegeLevel>()
                .HasOne<PrivilegeLevel>(upl => upl.PrivilegeLevel)
                .WithMany(pl => pl.UserPrivilegeLevels)
                .HasForeignKey(upl => upl.PrivilegeLevelId);


            modelBuilder.Entity<MemberRole>().HasKey(mr => new { mr.RoleId, mr.MemberId });

            modelBuilder.Entity<MemberRole>()
                .HasOne<Member>(mr => mr.Member)
                .WithMany(m => m.MemberRoles)
                .HasForeignKey(mr => mr.MemberId);


            modelBuilder.Entity<MemberRole>()
                .HasOne<Role>(mr => mr.Role)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(mr => mr.RoleId);


            modelBuilder.Entity<MemberPrivilegeLevel>().HasKey(mpl => new { mpl.PrivilegeLevelId, mpl.MemberId });

            modelBuilder.Entity<MemberPrivilegeLevel>()
                .HasOne<Member>(mpl => mpl.Member)
                .WithMany(m => m.MemberPrivilegeLevels)
                .HasForeignKey(mpl => mpl.MemberId);


            modelBuilder.Entity<MemberPrivilegeLevel>()
                .HasOne<PrivilegeLevel>(mpl => mpl.PrivilegeLevel)
                .WithMany(pl => pl.MemberPrivilegeLevels)
                .HasForeignKey(mpl => mpl.PrivilegeLevelId);

            //These are for seeding initial data

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

            //modelBuilder.Entity<Role>().HasData(
            //    new Role 
            //    {
            //        RoleTitle = "Admin"
            //    }
            //    );

            //modelBuilder.Entity<PrivilegeLevel>().HasData(
            //    new PrivilegeLevel 
            //    {
            //        PrivilegeLevelTitle = "Level 1"
            //    }
            //    );
        }
    }
}
