using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Models;
using ProjectManagement.Api.Models.Enums;
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

        [HttpGet("{projectId:int}")]
        public IActionResult Get(int projectId) 
        {
            var project = new Project()
            {
                EndDate = DateTime.Now.AddDays(30),
                StartDate = DateTime.Now,
                CreationDate = DateTime.Now.AddDays(-1),
                LastModifiedDate = DateTime.Now,
                ModifiedBy = "Hamid Barani",
                Name = "Sample Project for Getting",
                Priority = Priority.Low
            };

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Project project) 
        {
            var obj = new Project()
            {
                EndDate = DateTime.Now.AddDays(30),
                StartDate = DateTime.Now,
                CreationDate = DateTime.Now.AddDays(-1),
                LastModifiedDate = DateTime.Now,
                ModifiedBy = "Hamid Barani",
                Name = "Sample Project for Posting",
                Priority = Priority.Low
            };

            return Ok();
        }
    }
}
