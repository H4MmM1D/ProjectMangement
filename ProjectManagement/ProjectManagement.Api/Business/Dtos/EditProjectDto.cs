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
    }

    public class SetEstimatedDeliveryDto 
    {
        public Guid Id { get; set; }

        public DateTime EstimatedDelivery { get; set; }
    }
}
