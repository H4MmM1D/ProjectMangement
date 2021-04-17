using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? Assigny { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
