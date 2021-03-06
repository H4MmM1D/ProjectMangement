﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Business
{
    public class EntityBase
    {
        public EntityBase() 
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            ModifiedBy = "Admin";    
        }

        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
