using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class Role : EntityBase
    {
        public Role() : base()
        {

        }

        [Display(Name = "")]
        [Required(ErrorMessage = "عنوان می بایست وارد شود.")]
        public string RoleTitle { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }

        public virtual List<MemberRole> MemberRoles { get; set; }

    }
}
