using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToochiChat.Application.Interfaces;

namespace ToochiChat.API.Controllers;

[Authorize]
[ApiController]
[Route(("api/v1/messages"))]
public sealed class MessageController : Controller
{
    private readonly IMessageService _messageService;
    private readonly ILogger<MessageController> _logger;

    public MessageController(IMessageService messageService, ILogger<MessageController> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }
    
    
}