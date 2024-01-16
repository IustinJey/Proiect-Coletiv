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

        // Constructor to inject IUserService dependency
        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // Retrieves a user by ID
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

        // Retrieves a user by username
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

        // Retrieves a user by email address
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

        // Retrieves all users
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // Retrieves jobs associated with a user ID
        [HttpGet("{userId}/jobs")]
        public async Task<IActionResult> GetJobsByUserId(int userId)
        {
            var jobs = await _userService.GetJobsByUserIdAsync(userId);

            return Ok(jobs);
        }

        // Retrieves reviews associated with a user ID
        [HttpGet("{userId}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var reviews = await _userService.GetReviewsByUserIdAsync(userId);

            return Ok(reviews);
        }

        // Retrieves certificates associated with a user ID
        [HttpGet("{userId}/certificates")]
        public async Task<IActionResult> GetUserCertificatesByUserId(int userId)
        {
            var certificates = await _userService.GetUserCertificatesByUserIdAsync(userId);

            return Ok(certificates);
        }

        [HttpGet("certificates")]
        public async Task<ActionResult<List<CertificatUser>>> GetAllCertificatesAsync()
        {
            var certificates = await _userService.GetAllCertificatesAsync();
            return Ok(certificates);
        }

        [HttpGet("{userId}/profile-picture")]
        public async Task<ActionResult<string>> GetUserProfilePictureAsync(int userId)
        {
            var profilePictureUrl = await _userService.GetProfilePictureUrl(userId);
            if (profilePictureUrl != null)
            {
                return Ok(profilePictureUrl);
            }

            return NotFound();
        }

        // Retrieves badges associated with a user ID
        [HttpGet("{userId}/badges")]
        public async Task<IActionResult> GetUserBadgesByUserId(int userId)
        {
            var badges = await _userService.GetUserBadgesByUserIdAsync(userId);

            return Ok(badges);
        }

        // Updates an existing user
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto userDto)
        {
            // Validate the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve the existing user
            var existingUser = await _userService.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Update user properties with values from the DTO
            existingUser.Username = userDto.Username.ToLower();
            existingUser.Email = userDto.Email.ToLower();
            existingUser.PhoneNumber = userDto.PhoneNumber.ToLower();
            existingUser.Location = userDto.Location.ToLower();
            existingUser.Age = userDto.Age;
            existingUser.Verified = userDto.Verified;
            existingUser.ProfilePicture = userDto.ProfilePicture;

            // Update the user asynchronously, including the password
            await _userService.UpdateUserAsync(existingUser, userDto.Password);

            // Create a new DTO with updated user information
            var updatedUserDto = new UserUpdateDto
            {
                Username = existingUser.Username,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Location = existingUser.Location,
                Age = existingUser.Age,
                Verified = existingUser.Verified,
                ProfilePicture = existingUser.ProfilePicture
            };

            return Ok(updatedUserDto);
        }

        // Deletes a user by ID
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            // Retrieve the user
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Delete the user asynchronously
            await _userService.DeleteUserAsync(userId);

            return NoContent();
        }
    }
}
