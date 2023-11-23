using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skillz_backend.models;
using skillz_backend.Services;

namespace skillz_backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;


        private static List<string> InvalidTokens = new List<string>();

        public AuthenticationController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetUserByUsernameAsync(userModel.Username);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Username already taken." });
            }

            existingUser = await _userService.GetUserByEmailAsync(userModel.Email);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Email already in use." });
            }

            var newUser = new User
            {
                Username = userModel.Username,
                Email = userModel.Email,
                Password = userModel.Password
            };

            await _userService.CreateUserAsync(newUser);

            var token = _authenticationService.GenerateToken(newUser);

            return Ok(new { Token = token, Message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userModel)
        {
            var user = await _userService.GetUserByUsernameAsync(userModel.Username);

            if (user == null || !_authenticationService.ValidatePassword(userModel.Password, user.Password, user.Salt))
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            var token = _authenticationService.GenerateToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Invalid token." });
            }

            // Verifică dacă tokenul este deja în listă (invalidat)
            bool isTokenInvalidated;
            lock (InvalidTokens)
            {
                isTokenInvalidated = InvalidTokens.Contains(token);
                if (!isTokenInvalidated)
                {
                    // Adaugă tokenul la lista de tokenuri invalide
                    InvalidTokens.Add(token);
                }
            }

            if (isTokenInvalidated)
            {
                return BadRequest(new { Message = "Token already invalidated." });
            }

            return Ok(new { Message = "Logout successful." });
        }


    }
}
