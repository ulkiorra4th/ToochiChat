using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using ToochiChat.API.ClientHubInterfaces;
using ToochiChat.API.RequestModels;
using ToochiChat.Application.Auth.Interfaces;
using ToochiChat.Application.Interfaces;

namespace ToochiChat.API.Hubs;

// TODO: store the user connection id in database 
[Authorize]
internal class ChatHub : Hub<IChatClient>
{
    // TODO: save current users opened chat in cache (users in chat, some messages, etc..)
    private readonly IDistributedCache _cache;
    private readonly IChatService _chatService;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;

    public ChatHub(IDistributedCache cache, IChatService chatService, IUserService userService, 
        IAccountService accountService)
    {
        _cache = cache;
        _chatService = chatService;
        _userService = userService;
        _accountService = accountService;
    }

    public async Task JoinChat(ChatConnectionModel connection)
    {
        // TODO: think about transaction
        // TODO: save connectionId with userId in database

        await _chatService.AddChatMember(connection.AccountId, connection.ChatId);
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatId);

        await Clients.Group(connection.ChatId)
            .ReceiveMessage("Admin", $"{connection.AccountId} joined to chat");
    }

    public async Task LeaveChat(ChatConnectionModel connection)
    {
        throw new NotImplementedException();
    }

    public async Task ConnectToChat(ChatConnectionModel connection)
    {
        // TODO: store connectionId from database by userId 
        // TODO: save other users in chat in cache (redis)

        throw new NotImplementedException();
    }

    public async Task SendMessage(MessageSendModel messageSendModel)
    {
        // TODO: think about adding abstraction layer or adding low level message sending
        //await _chatService.SendMessageFrom(messageSendModel.AccountId, )
        
        throw new NotImplementedException();
    }

    public async Task DeleteMessage()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMessage()
    {
        throw new NotImplementedException();
    }
}