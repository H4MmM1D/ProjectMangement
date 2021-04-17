using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Business;
using ProjectManagement.Api.Business.Dtos.User;
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
    [ApiExplorerSettings(GroupName = "UserOpenApi")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        [HttpGet("{userId:Guid}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(Guid userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return NotFound("کاربری ای یاقت نشد.");
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users.OrderByDescending(x => x.CreationDate).ToList();
            var userDtos = new List<UserDto>();

            foreach (var item in users)
            {
                userDtos.Add(_mapper.Map<UserDto>(item));
            }

            return Ok(userDtos);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult AddUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailExists = _db.Users.Any(x => x.Email == userDto.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "ایمیل قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var usernameExists = _db.Users.Any(x => x.Username == userDto.Username);

            if (usernameExists)
            {
                ModelState.AddModelError("Username", "نام کاربری قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var userTitleExists = _db.Users.Any(x => x.UserTitle == userDto.UserTitle);

            if (userTitleExists)
            {
                ModelState.AddModelError("UserTitle", "عنوان کاربری قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            userDto.Password = PasswordHelper.EncodePasswordMd5(userDto.Password);

            var user = _mapper.Map<Api.Business.User>(userDto);

            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            var userResult = _mapper.Map<UserDto>(user);

            return CreatedAtRoute("GetUser", new { userId = user.Id }, userResult);
        }

        [HttpPut("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(Guid userId, [FromBody] EditUserDto userDto)
        {
            if (userDto == null || userId != userDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = _db.Users.Any(x => x.Id == userId);

            if (!userExists)
                return NotFound("کاربر یافت نشد.");

            var emailExists = _db.Users.Any(x => x.Id != userDto.Id && x.Email == userDto.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "ایمیل قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var usernameExists = _db.Users.Any(x => x.Id != userDto.Id && x.Username == userDto.Username);

            if (usernameExists)
            {
                ModelState.AddModelError("Email", "نام کاربری قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var userTitleExists = _db.Users.Any(x => x.Id != userDto.Id && x.UserTitle == userDto.UserTitle);

            if (userTitleExists)
            {
                ModelState.AddModelError("Email", "عنوان کاربری قبلا استفاده شده است.");
                return StatusCode(409, ModelState);
            }

            var user = _mapper.Map<User>(userDto);

            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("[action]/{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(Guid userId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return NotFound("کاربری یافت نشد.");
            }
            try
            {
                _db.Users.Remove(user);
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
