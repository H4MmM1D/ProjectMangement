using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class MemberPrivilegeLevel : EntityBase
    {
        MemberPrivilegeLevel() : base() 
        {

        }

        public Guid MemberId { get; set; }

        public Guid PrivilegeLevelId { get; set; }

        public virtual Member Member { get; set; }

        public virtual PrivilegeLevel PrivilegeLevel { get; set; }
    }
}
