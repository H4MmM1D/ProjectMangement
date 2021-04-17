using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos.Member
{
    public class CreateMemberDto
    {
        [Required(ErrorMessage = "عنوان اعضا می بایست وارد شود.")]
        public string MemberTitle { get; set; }

        [Required(ErrorMessage = "نام کاربری می بایست وارد شود.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "کلمه عبور می بایست وارد شود.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ایمیل می بایست وارد شود.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
    }
}
