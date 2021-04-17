using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos.Meeting
{
    public class MeetingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Report { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndData { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
