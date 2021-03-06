﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos.Project;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
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
            var project = _db.Projects.SingleOrDefault(x => x.Id == projectId);

            if (project == null)
            {
                return NotFound("پروژه ای یاقت نشد.");
            }

            var projectDto = _mapper.Map<ProjectDto>(project);

            return Ok(projectDto);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<ProjectDto>))]
        public IActionResult GetAllProjects()
        {
            var projects = _db.Projects.OrderByDescending(x => x.CreationDate).ToList();
            var projectDtos = new List<ProjectDto>();

            foreach (var item in projects)
            {
                projectDtos.Add(_mapper.Map<ProjectDto>(item));
            }

            return Ok(projectDtos);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult AddProject([FromBody] CreateProjectDto projectDto)
        {
            if (projectDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(Priority), projectDto.Priority))
            {
                ModelState.AddModelError("", "اولویت پروژه مشخص نشده است.");
                return StatusCode(400, ModelState);
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

            var projectResult = _mapper.Map<ProjectDto>(project);

            return CreatedAtRoute("GetProject", new { projectId = project.Id }, projectResult);
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

            var projectExists = _db.Projects.Any(x => x.Id == projectId);

            if (!projectExists)
                return NotFound("پروژه یافت نشد.");

            var projectNameExists = _db.Projects.Any(x => x.Id != projectDto.Id && x.Name == projectDto.Name);

            if (projectNameExists)
            {
                ModelState.AddModelError("", "نام پروژه تکراری می باشد.");
                return StatusCode(409, ModelState);
            }

            if (!Enum.IsDefined(typeof(Priority), projectDto.Priority))
            {
                ModelState.AddModelError("", "اولویت پروژه مشخص نشده است.");
                return StatusCode(400, ModelState);
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

        [HttpDelete("[action]/{projectId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProject(Guid projectId)
        {
            var project = _db.Projects.Include(x => x.Tasks).SingleOrDefault(x => x.Id == projectId);

            if (project == null)
            {
                return NotFound("پروژه ای یافت نشد.");
            }
            try
            {
                _db.Database.BeginTransaction();

                if (project.Tasks.Any())
                {
                    foreach (var item in project.Tasks)
                    {
                        item.ProjectId = null;
                    }

                    _db.Tasks.UpdateRange(project.Tasks);
                    _db.SaveChanges();
                }

                _db.Projects.Remove(project);
                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
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
            if (projectDto == null || projectId != projectDto.Id || !ModelState.IsValid)
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

        [HttpPatch("[action]/{projectId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SetProjectPriority(Guid projectId, [FromBody] SetProjectPriorityDto projectDto)
        {
            if (projectDto == null || projectId != projectDto.Id || !ModelState.IsValid)
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
                project.Priority = projectDto.Priority;
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
