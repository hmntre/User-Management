/*
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.DTOs;
using UserManagement.Api.Services;
using System.Security.Claims;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.LoginAsync(loginDto);

        if (!result.Success)
        {
            return Unauthorized(new { message = result.Message });
        }

        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(emailClaim))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _authService.GetCurrentUserAsync(emailClaim);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        return Ok(user);
    }
}
*/