using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos.Member;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MemberOpenApi")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class MemberController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MemberController(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet("{memberId:Guid}", Name = "GetMember")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(Guid memberId)
        {
            var member = _db.Members.SingleOrDefault(x => x.Id == memberId);

            if (member == null)
            {
                return NotFound("عضوی یاقت نشد.");
            }

            var memberDto = _mapper.Map<MemberDto>(member);

            return Ok(memberDto);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<MemberDto>))]
        public IActionResult GetAllMembers()
        {
            var members = _db.Members.OrderByDescending(x => x.CreationDate).ToList();
            var memberDtos = new List<MemberDto>();

            foreach (var item in members)
            {
                memberDtos.Add(_mapper.Map<MemberDto>(item));
            }

            return Ok(memberDtos);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MemberDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult AddMember([FromBody] CreateMemberDto memberDto)
        {
            if (memberDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailExists = _db.Members.Any(x => x.Email == memberDto.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "ایمیل قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var usernameExists = _db.Members.Any(x => x.Username == memberDto.Username);

            if (usernameExists)
            {
                ModelState.AddModelError("Username", "نام کاربری قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var memberTitleExists = _db.Members.Any(x => x.MemberTitle == memberDto.MemberTitle);

            if (memberTitleExists)
            {
                ModelState.AddModelError("MemberTitle", "عنوان قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            memberDto.Password = PasswordHelper.EncodePasswordMd5(memberDto.Password);

            var member = _mapper.Map<Member>(memberDto);

            try
            {
                _db.Members.Add(member);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            var memberResult = _mapper.Map<MemberDto>(member);

            return CreatedAtRoute("GetMember", new { memberId = member.Id }, memberResult);
        }

        [HttpPut("[action]/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMember(Guid memberId, [FromBody] EditMemberDto memberDto)
        {
            if (memberDto == null || memberId != memberDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memberExists = _db.Members.Any(x => x.Id == memberId);

            if (!memberExists)
                return NotFound("عضو یافت نشد.");

            var emailExists = _db.Members.Any(x => x.Id != memberDto.Id && x.Email == memberDto.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "ایمیل قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var usernameExists = _db.Members.Any(x => x.Id != memberDto.Id && x.Username == memberDto.Username);

            if (usernameExists)
            {
                ModelState.AddModelError("Email", "عنوان قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var memberTitleExists = _db.Members.Any(x => x.Id != memberDto.Id && x.MemberTitle == memberDto.MemberTitle);

            if (memberTitleExists)
            {
                ModelState.AddModelError("Email", "عنوان قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var member = _mapper.Map<Member>(memberDto);

            try
            {
                _db.Members.Update(member);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("[action]/{memberId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMember(Guid memberId)
        {
            var member = _db.Members.SingleOrDefault(x => x.Id == memberId);

            if (member == null)
            {
                return NotFound("عضوی یافت نشد.");
            }
            try
            {
                _db.Members.Remove(member);
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
