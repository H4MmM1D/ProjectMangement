using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class MemberRole : EntityBase
    {
        public MemberRole() : base()
        {

        }

        public Guid MemberId { get; set; }

        public Guid RoleId { get; set; }

        public virtual Member Member { get; set; }

        public virtual Role Role { get; set; }
    }
}
