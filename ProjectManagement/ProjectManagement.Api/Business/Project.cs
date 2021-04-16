using ProjectManagement.Api.Business;
using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Api.Business
{
    public class Project : EntityBase
    {
        public Project() : base() 
        {

        }

        [Required]
        public string Name { get; set; }

        public DateTime EstimatedDelivery { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public virtual List<Task> Tasks { get; set; }
    }
}
