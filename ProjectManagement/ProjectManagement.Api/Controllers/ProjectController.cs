using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProjectController(ApplicationDbContext db) 
        {
            this._db = db;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var project = _db.Projects.FirstOrDefault();
            return Ok(project);
        }
    }
}
