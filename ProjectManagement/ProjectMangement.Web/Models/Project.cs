using ProjectMangement.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMangement.Web.Models
{
    public class Project
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should choose one these items")]
        public Priority Priority { get; set; }
    }
}
