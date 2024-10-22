using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using ToochiChat.API.RequestModels.Chat;
using ToochiChat.Application.Interfaces;

namespace ToochiChat.API.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/chats")]
public sealed class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;

    public ChatController(ILogger<ChatController> logger, IChatService chatService, IUserService userService, 
        IMessageService messageService)
    {
        _logger = logger;
        _chatService = chatService;
        _userService = userService;
        _messageService = messageService;
    }

    [HttpGet("{chatId}")]
    public async Task<IActionResult> GetChat(Guid chatId)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}/{startPage:int}/{pagesCount:int}")]
    public async Task<IActionResult> GetChat(Guid id, int startPage, int pagesCount)
    {
        if (!ModelState.IsValid) return BadRequest();        
        throw new NotImplementedException();
    }
    
    [HttpGet("{startPage:int}/{pagesCount:int}")]
    public async Task<IActionResult> GetChats(int startPage, int pagesCount)
    {
        if (!ModelState.IsValid) return BadRequest();

        throw new NotImplementedException();
    }
    
    [HttpGet("user-chats/{userId:guid}/{startPage:int}/{pagesCount:int}")]
    public async Task<IActionResult> GetUserChats([FromQuery] int startPage, [FromQuery] int pagesCount, 
        [FromQuery] Guid userId)
    {
        if (!ModelState.IsValid) return BadRequest();        
        throw new NotImplementedException();
    }

    [HttpGet("{chatId:guid}/messages/{messageId}")]
    public async Task<IActionResult> GetChatMessage(Guid chatId, ulong messageId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{chatId:guid}/messages")]
    public async Task<IActionResult> GetChatMessages(Guid chatId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Create([FromBody] CreateChatRequestModel model)
    {
        if (!ModelState.IsValid) return BadRequest();        
        throw new NotImplementedException();
    }

    [HttpDelete("user/{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromQuery] Guid id, [FromBody] DeleteUserFromChatRequestModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        throw new NotImplementedException();
    }

    [HttpPut("edit-title/{id:guid}")]
    public async Task<IActionResult> EditTitle([FromQuery] Guid id, [FromBody] EditChatTitleRequestModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteChat([FromQuery] Guid id, [FromBody] Guid userId)
    {
        if (!ModelState.IsValid) return BadRequest();
        throw new NotImplementedException();
    }
    
}