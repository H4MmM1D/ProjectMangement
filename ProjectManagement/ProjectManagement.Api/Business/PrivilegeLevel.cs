using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class PrivilegeLevel : EntityBase
    {
        public PrivilegeLevel() : base() 
        {

        }

        [Required(ErrorMessage = "عنوان می بایست وارد شود.")]
        public string PrivilegeLevelTitle { get; set; }

        public virtual List<UserPrivilegeLevel> UserPrivilegeLevels { get; set; }

        public virtual List<MemberPrivilegeLevel> MemberPrivilegeLevels { get; set; }
    }
}
