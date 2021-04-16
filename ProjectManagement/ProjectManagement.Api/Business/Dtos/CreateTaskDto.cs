using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business.Dtos
{
    public class CreateTaskDto
    {
        public Guid? ProjectId { get; set; }
        public Guid? Assigny { get; set; }

        [Required(ErrorMessage = "عنوان تسک می بایست مشخص گردد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "تاریخ شروع تسک می بایست مشخص گردد.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "تاریخ پایان پروژه می بایست مشخص گردد.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "اولویت تسک می بایست مشخص گردد.")]
        public Priority Priority { get; set; }
    }
}
