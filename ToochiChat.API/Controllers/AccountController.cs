using Microsoft.AspNetCore.Mvc;
using ToochiChat.API.RequestModels;
using ToochiChat.Application.Auth.Interfaces;
using ToochiChat.Application.Interfaces;

namespace ToochiChat.API.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IUserService userService, IAccountService accountService)
    {
        _logger = logger;
        _userService = userService;
        _accountService = accountService;
    }
    
    /// <summary>
    /// Gets the user
    /// </summary>
    /// <param name="id">Id of the user</param>
    /// <returns>User</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var userResult = await _userService.Get(id);

        if (userResult.IsFailure)
        {
            _logger.LogError(userResult.Error);
            return Problem();
        }

        return Ok(userResult.Value);
    }

    /// <summary>
    /// Logs in the user
    /// </summary>
    /// <param name="request">Login model</param>
    /// <returns>JWT token in cookies</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        var tokenResult = await _accountService.LogIn(request.Email, request.Password);
        
        if (tokenResult.IsFailure)
        {
            _logger.LogError(tokenResult.Error);
            return Problem();
        }
        
        Response.Cookies.Append("some_name", tokenResult.Value);
        return Ok();
    }

    /// <summary>
    /// Registers the user
    /// </summary>
    /// <param name="request">Register model</param>
    /// <returns>JWT token in cookies</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel request)
    {
        var tokenResult = await _accountService.Register(request.Email, request.Password, request.BirthDate);
        if (tokenResult.IsFailure)
        {
            _logger.LogError(tokenResult.Error);
            return Problem();
        }
        
        Response.Cookies.Append("some_name", tokenResult.Value);
        return Ok();
    }
}