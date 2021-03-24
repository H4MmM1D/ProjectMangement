using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos;
using ProjectManagement.Api.Data;
using System;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ProjectOpenApi")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProjectController(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet("{projectId:Guid}", Name = "GetProject")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(Guid projectId) 
        {
            var project = _db.Projects.FirstOrDefault(x => x.Id == projectId);

            if (project == null) 
            {
                return NotFound();
            }
                
            var projectDto = _mapper.Map<ProjectDto>(project);

            return Ok(projectDto);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateProjectDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult AddProject([FromBody] CreateProjectDto projectDto) 
        {
            if (projectDto == null || !ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var projectExists = _db.Projects.Any(x => x.Name == projectDto.Name);
            if (projectExists) 
            {
                ModelState.AddModelError("", "نام پروژه تکراری می باشد.");
                return StatusCode(409, ModelState);
            }

            var project = _mapper.Map<Project>(projectDto);

            try
            {
                _db.Projects.Add(project);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProject", new { projectId = project.Id }, project);
        }

        [HttpPut("[action]/{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProject(Guid projectId, [FromBody] EditProjectDto projectDto) 
        {
            if (projectDto == null || projectId != projectDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = _mapper.Map<Project>(projectDto);

            try
            {
                _db.Projects.Update(project);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpPatch("[action]/{projectId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SetProjectEstiamtedTime(Guid projectId, [FromBody] SetEstimatedDeliveryDto projectDto) 
        {
            if (projectDto == null || projectId != projectDto.Id) 
            {
                return BadRequest(ModelState);
            }

            var project = _db.Projects.FirstOrDefault(x => x.Id == projectId);

            if (project == null) 
            {
                return NotFound();
            }

            try
            {
                project.EstimatedDelivery = projectDto.EstimatedDelivery;
                _db.Projects.Update(project);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
