using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToochiChat.API.Controllers;

[Authorize]
[ApiController]
[Route(("api/v1/messages"))]
public sealed class MessageController : Controller
{
}