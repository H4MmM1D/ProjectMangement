using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos
{
    public class EditProjectDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام پروژه اجباری می باشد.")]
        public string Name { get; set; }

        public DateTime EstimatedDelivery { get; set; }

        [Required(ErrorMessage = "تاریخ پایان می بایست وارد شود.")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "اولویت پروژه می بایست مشخص گردد.")]
        public Priority Priority { get; set; }
    }

    public class SetEstimatedDeliveryDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "تاریخ تحویل مشخص نشده است.")]
        public DateTime EstimatedDelivery { get; set; }
    }

    public class SetProjectPriorityDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "اولویت پروژه می بایست مشخص گردد.")]
        public Priority Priority { get; set; }
    }
}
