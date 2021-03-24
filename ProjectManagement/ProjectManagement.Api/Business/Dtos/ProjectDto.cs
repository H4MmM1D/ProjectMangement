using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime EstimatedDelivery { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        
    }
}
