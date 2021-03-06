﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos.Task;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "TaskOpenApi")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TaskController(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet("{taskId:Guid}", Name = "GetTask")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(Guid taskId)
        {
            var task = _db.Tasks.FirstOrDefault(x => x.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            var taskDto = _mapper.Map<TaskDto>(task);

            return Ok(taskDto);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<TaskDto>))]
        public IActionResult GetAllTasks()
        {
            var tasks = _db.Tasks.OrderByDescending(x => x.CreationDate).ToList();
            var taskDtos = new List<TaskDto>();

            foreach (var item in tasks)
            {
                taskDtos.Add(_mapper.Map<TaskDto>(item));
            }

            return Ok(taskDtos);
        }

        [HttpGet("[action]/{projectId:Guid}")]
        [ProducesResponseType(200, Type = typeof(List<TaskDto>))]
        public IActionResult GetProjectTasks(Guid projectId)
        {
            var project = _db.Projects.SingleOrDefault(x => x.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            var tasks = _db.Tasks.Where(x => x.ProjectId == projectId).ToList();
            var taskDtos = new List<TaskDto>();

            foreach (var item in tasks)
            {
                taskDtos.Add(_mapper.Map<TaskDto>(item));
            }

            return Ok(taskDtos);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult AddTask([FromBody] CreateTaskDto taskDto)
        {
            if (taskDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(Priority), taskDto.Priority))
            {
                ModelState.AddModelError("", "اولویت تسک مشخص نشده است.");
                return StatusCode(400, ModelState);
            }

            if (taskDto.ProjectId != null)
            {
                var projectExists = _db.Projects.Any(x => x.Id == taskDto.ProjectId);
                if (!projectExists)
                {
                    return NotFound("پروژه یافت نشد.");
                }
            }

            var taskExists = _db.Tasks.Any(x => x.Name == taskDto.Name);

            if (taskExists)
            {
                ModelState.AddModelError("", "نام تسک تکراری می باشد.");
                return StatusCode(409, ModelState);
            }

            var task = _mapper.Map<Task>(taskDto);

            try
            {
                _db.Tasks.Add(task);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            var taskResult = _mapper.Map<TaskDto>(task);

            return CreatedAtRoute("GetTask", new { taskId = task.Id }, taskResult);
        }

        [HttpPut("[action]/{taskId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTask(Guid taskId, [FromBody] EditTaskDto taskDto)
        {
            if (taskDto == null || taskId != taskDto.Id)
            {
                return BadRequest(ModelState);
            }

            var taskExists = _db.Tasks.Any(x => x.Id == taskId);

            if (!taskExists)
                return NotFound("تسک یافت نشد.");

            var taskNameExists = _db.Tasks.Any(x => x.Id != taskDto.Id && x.Name == taskDto.Name);

            if (taskNameExists)
            {
                ModelState.AddModelError("", "نام تسک تکراری می باشد.");
                return StatusCode(409, ModelState);
            }

            if (taskDto.ProjectId != null)
            {
                var projectExists = _db.Projects.Any(x => x.Id == taskDto.ProjectId);
                if (!projectExists)
                {
                    return NotFound("پروژه یافت نشد.");
                }
            }

            var task = _mapper.Map<Task>(taskDto);

            try
            {
                _db.Tasks.Update(task);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("[action]/{taskId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTask(Guid taskId)
        {
            var task = _db.Tasks.SingleOrDefault(x => x.Id == taskId);

            if (task == null)
            {
                return NotFound("تسکی یافت نشد.");
            }
            try
            {
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("[action]/{projectId:Guid}/{taskId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AssignTaskToProject(Guid projectId, Guid taskId)
        {
            var task = _db.Tasks.SingleOrDefault(x => x.Id == taskId);
            var project = _db.Projects.SingleOrDefault(x => x.Id == projectId);

            if (task == null || project == null)
            {
                return NotFound();
            }

            try
            {
                task.ProjectId = projectId;
                _db.Tasks.Update(task);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("[action]/{userId:Guid}/{taskId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AssignTaskToUser(Guid userId, Guid taskId)
        {
            var task = _db.Tasks.SingleOrDefault(x => x.Id == taskId);
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);

            if (task == null || user == null)
            {
                return NotFound();
            }

            try
            {
                task.Assigny = userId;
                _db.Tasks.Update(task);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("[action]/{taskId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SetTaskPriority(Guid taskId, [FromBody] SetTaskPriorityDto taskDto)
        {
            if (taskDto == null || taskId != taskDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = _db.Tasks.FirstOrDefault(x => x.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            try
            {
                task.Priority = taskDto.Priority;
                _db.Tasks.Update(task);
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
