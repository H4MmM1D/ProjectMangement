using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserTitle { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<UserPrivilegeLevel> UserPrivilegeLevels { get; set; }
    }
}
