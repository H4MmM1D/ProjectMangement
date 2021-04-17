using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos.Meeting;
using ProjectManagement.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MeetingOpenApi")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class MeetingController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MeetingController(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet("{meetingId:Guid}", Name = "GetMeeting")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(Guid meetingId)
        {
            var meeting = _db.Meetings.SingleOrDefault(x => x.Id == meetingId);

            if (meeting == null)
            {
                return NotFound("جلسه یاقت نشد.");
            }

            var meetingDto = _mapper.Map<MeetingDto>(meeting);

            return Ok(meetingDto);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<MeetingDto>))]
        public IActionResult GetAllMeetings()
        {
            var meetings = _db.Meetings.OrderByDescending(x => x.CreationDate).ToList();
            var meetingDtos = new List<MeetingDto>();

            foreach (var item in meetings)
            {
                meetingDtos.Add(_mapper.Map<MeetingDto>(item));
            }

            return Ok(meetingDtos);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MeetingDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult AddMeeting([FromBody] CreateMeetingDto meetingDto)
        {
            if (meetingDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meetingTitleExists = _db.Meetings.Any(x => x.Title == meetingDto.Title);

            if (meetingTitleExists)
            {
                ModelState.AddModelError("MeetingTitle", "عنوان قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var meeting = _mapper.Map<Meeting>(meetingDto);

            try
            {
                _db.Meetings.Add(meeting);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            var meetingResult = _mapper.Map<MeetingDto>(meeting);

            return CreatedAtRoute("GetMeeting", new { meetingId = meeting.Id }, meetingResult);
        }

        [HttpPut("[action]/{meetingId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMeeting(Guid meetingId, [FromBody] EditMeetingDto meetingDto)
        {
            if (meetingDto == null || meetingId != meetingDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meetingExists = _db.Meetings.Any(x => x.Id == meetingId);

            if (!meetingExists)
                return NotFound("جلسه یافت نشد.");

            var meetingTitleExists = _db.Meetings.Any(x => x.Id != meetingDto.Id && x.Title == meetingDto.Title);

            if (meetingTitleExists)
            {
                ModelState.AddModelError("MeetingTitle", "عنوان قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var meeting = _mapper.Map<Meeting>(meetingDto);

            try
            {
                _db.Meetings.Update(meeting);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("[action]/{meetingId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMeeting(Guid meetingId)
        {
            var meeting = _db.Meetings.SingleOrDefault(x => x.Id == meetingId);

            if (meeting == null)
            {
                return NotFound("جلسه یافت نشد.");
            }
            try
            {
                _db.Meetings.Remove(meeting);
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
