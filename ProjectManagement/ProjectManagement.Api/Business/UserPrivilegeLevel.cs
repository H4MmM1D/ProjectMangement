using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class UserPrivilegeLevel : EntityBase
    {
        public UserPrivilegeLevel() : base() 
        {

        }

        public Guid UserId { get; set; }

        public Guid PrivilegeLevelId { get; set; }

        public virtual User User { get; set; }

        public virtual PrivilegeLevel PrivilegeLevel { get; set; }
    }
}
