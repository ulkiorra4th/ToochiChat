using Microsoft.AspNetCore.Mvc;
using ToochiChat.API.RequestModels.User;
using ToochiChat.Application.Interfaces;

namespace ToochiChat.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public sealed class UserController : Controller
{
   private readonly ILogger<UserController> _logger;
   private readonly IUserService _userService;

   public UserController(ILogger<UserController> logger, IUserService userService)
   {
      _logger = logger;
      _userService = userService;
   }

   [HttpGet("{userName}")]
   public async Task<IActionResult> GetUser(string userName, CancellationToken cancellationToken)
   {
      if (!ModelState.IsValid) return BadRequest();      
      throw new NotImplementedException();
   }
   
   [HttpGet("{startPage}/{pagesCount}")]
   public async Task<IActionResult> GetAllUsers(int startPage, int pagesCount, CancellationToken cancellationToken)
   {
      if (!ModelState.IsValid) return BadRequest();      
      throw new NotImplementedException();
   }
   
   [HttpPut("{userName}")]
   public async Task<IActionResult> UpdateUser([FromQuery] string userName, [FromBody] UserUpdateRequestModel model)
   {
      if (!ModelState.IsValid) return BadRequest();      
      throw new NotImplementedException();
   }

   [HttpPut("update-username/{id}")]
   public async Task<IActionResult> UpdateUserName([FromQuery] Guid id, [FromBody] string newUserName)
   {
      if (!ModelState.IsValid) return BadRequest();
      throw new NotImplementedException();
   }
}