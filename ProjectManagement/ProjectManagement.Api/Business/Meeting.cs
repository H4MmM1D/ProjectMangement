﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class Meeting : EntityBase
    {
        [Required(ErrorMessage = "عنوان اجباری می باشد.")]
        public string Title { get; set; }

        public string Report { get; set; }

        [Required(ErrorMessage = "تاریخ آغاز اجباری می باشد.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "تاریخ پایان اجباری می باشد.")]
        public DateTime EndData { get; set; }
    }
}
