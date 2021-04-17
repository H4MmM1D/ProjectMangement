using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos.Member
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string MemberTitle { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<MemberRole> MemberRoles { get; set; }
        public List<MemberPrivilegeLevel> MemberPrivilegeLevels { get; set; }
    }
}
