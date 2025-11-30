using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Taskly.Application.Interfaces;
using Taskly.Application.DTOs;

namespace Taskly.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Application.DTOs.RegisterRequest req)
        {
            var resp = await _auth.RegisterAsync(req);
            return CreatedAtAction(null, resp);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Application.DTOs.LoginRequest req)
        {
            var resp = await _auth.LoginAsync(req);
            return Ok(resp);
        }
    }
}
