using ProjectManagement.Api.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Api.Business
{
    public class Task : EntityBase
    {
        public Task() : base() 
        {

        }

        public Guid? ProjectId { get; set; }
        public Guid? Assigny { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }
}
