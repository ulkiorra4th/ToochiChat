using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToochiChat.API.RequestModels.Chat;
using ToochiChat.Application.Interfaces;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.API.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/chats")]
public sealed class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;

    public ChatController(ILogger<ChatController> logger, IChatService chatService, IMessageService messageService)
    {
        _logger = logger;
        _chatService = chatService;
        _messageService = messageService;
    }

    [HttpGet("{chatId:guid}")]
    public async Task<IActionResult> GetChat(Guid chatId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var chatResult = await _chatService.GetChatById(chatId);

        if (chatResult.IsFailure)
        {
            _logger.LogError(chatResult.Error);
            return Problem(chatResult.Error);
        }

        return Ok(chatResult.Value);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetChats([FromBody] int startPage, [FromBody] int pagesCount)
    {
        if (!ModelState.IsValid) return BadRequest();
        var chatsResult = await _chatService.GetRange(startPage, pagesCount);

        if (chatsResult.IsFailure)
        {
            _logger.LogError(chatsResult.Error);
            return Problem(chatsResult.Error);
        }

        return Ok(chatsResult.Value);
    }

    [HttpGet("{chatId:guid}/messages/{messageId}")]
    public async Task<IActionResult> GetChatMessage(Guid chatId, ulong messageId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var messageResult = await _messageService.Get(chatId, messageId);

        if (messageResult.IsFailure)
        {
            _logger.LogError(messageResult.Error);
            return Problem(messageResult.Error);
        }

        return Ok(messageResult.Value);
    }

    [HttpPost("{chatId:guid}/messages/list")]
    public async Task<IActionResult> GetChatMessages(Guid chatId, ulong offset, ulong count)
    {
        if (!ModelState.IsValid) return BadRequest();

        var messagesResult = await _messageService.GetRange(chatId, offset, count);

        if (messagesResult.IsFailure)
        {
            _logger.LogError(messagesResult.Error);
            return Problem(messagesResult.Error);
        }

        return Ok(messagesResult.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateChatRequestModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var owner = Domain.Models.User.CreateReference(model.OwnerId).Value;
        var chatResult = Chat.Create(Guid.NewGuid(), model.Title, owner, model.CreationDate);

        if (chatResult.IsFailure) return BadRequest(chatResult.Error);

        var chatCreatedResult = await _chatService.Create(chatResult.Value);

        if (chatCreatedResult.IsFailure)
        {
            _logger.LogError(chatCreatedResult.Error);
            return Problem(chatCreatedResult.Error);
        }

        return Ok(chatResult.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateChatRequestModel model)
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteChat([FromQuery] Guid id, [FromBody] Guid userId)
    {
        if (!ModelState.IsValid) return BadRequest();
        throw new NotImplementedException();
    }
}