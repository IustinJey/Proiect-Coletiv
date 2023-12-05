using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skillz_backend.DTOs;
using skillz_backend.models;
using skillz_backend.Services;

namespace skillz_backend.controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{userId}/jobs")]
        public async Task<IActionResult> GetJobsByUserId(int userId)
        {
            var jobs = await _userService.GetJobsByUserIdAsync(userId);

            return Ok(jobs);
        }

        [HttpGet("{userId}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var reviews = await _userService.GetReviewsByUserIdAsync(userId);

            return Ok(reviews);
        }

        [HttpGet("{userId}/certificates")]
        public async Task<IActionResult> GetUserCertificatesByUserId(int userId)
        {
            var certificates = await _userService.GetUserCertificatesByUserIdAsync(userId);

            return Ok(certificates);
        }

        [HttpGet("{userId}/badges")]
        public async Task<IActionResult> GetUserBadgesByUserId(int userId)
        {
            var badges = await _userService.GetUserBadgesByUserIdAsync(userId);

            return Ok(badges);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserWithPasswordDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = userDto.Username.ToLower(),
                Email = userDto.Email.ToLower(),
                PhoneNumber = userDto.PhoneNumber.ToLower(),
                Location = userDto.Location.ToLower(),
            };

            await _userService.CreateUserWithPlainTextPasswordAsync(user, userDto.Password);

            // Returnează un UserDto pentru a nu expune informații sensibile precum parolele
            var userDtoResponse = new UserDto
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Location = userDto.Location,
            };

            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, userDtoResponse);
        }



        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                return NotFound();
            }

            // You can add additional validation or business logic here

            user.UserId = userId; // Ensure the correct user ID is set
            await _userService.UpdateUserAsync(user);

            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(userId);

            return NoContent();
        }
    }
}
