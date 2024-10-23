using Microsoft.AspNetCore.Mvc;
using ToochiChat.API.RequestModels.Account;
using ToochiChat.Application.Auth.Interfaces;

namespace ToochiChat.API.Controllers;

[ApiController]
[Route("api/v1/account")]
public sealed class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }
    
    /// <summary>
    /// Logs the user in
    /// </summary>
    /// <param name="request">Login model</param>
    /// <returns>JWT token in cookies</returns>
    [HttpPost("sign-in")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var tokenResult = await _accountService.LogIn(request.Email, request.Password);
        
        if (tokenResult.IsFailure)
        {
            _logger.LogError(tokenResult.Error);
            return Problem();
        }
        
        Response.Cookies.Append("token", tokenResult.Value);
        return Ok();
    }

    /// <summary>
    /// Logs the user out
    /// </summary>
    /// <param name="model">LogOut Request Model</param>
    /// <returns></returns>
    [HttpPost("log-out")]
    public async Task<IActionResult> LogOut([FromBody] LogOutRequestModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Registers the user
    /// </summary>
    /// <param name="request">Register model</param>
    /// <returns>JWT token in cookies</returns>
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel request)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var tokenResult = await _accountService.Register(request.Email, request.Password, request.BirthDate);
        if (tokenResult.IsFailure)
        {
            _logger.LogError(tokenResult.Error);
            return Problem();
        }
        
        Response.Cookies.Append("token", tokenResult.Value);
        return Ok();
    }

    /// <summary>
    /// Sends verification code to accounts id
    /// </summary>
    /// <param name="model">Send Verification Code Request Model</param>
    /// <returns>Ok</returns>
    [HttpPost("send-verification-code")]
    public async Task<IActionResult> SendVerificationCode(SendVerificationCodeRequestModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        // TODO: check account id from cookies 
        
        _accountService.SendVerificationCode(model.Email);
        return Ok();
    }

    /// <summary>
    /// Verifies user's account
    /// </summary>
    /// <param name="request">Account verify request model</param>
    /// <returns>True if account was verified</returns>
    /// <returns>BadRequest if request is invalid</returns>
    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] AccountVerifyRequestModel request)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var confirmResult = await _accountService.Verify(request.Email, request.ConfirmationCode);

        if (confirmResult.IsFailure)
        {
            _logger.LogError(confirmResult.Error);
            return BadRequest(confirmResult.Error);
        }
        
        return Ok(confirmResult.Value);
    }
}