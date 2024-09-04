using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using ToochiChat.API.Hubs.ClientHubInterfaces;
using ToochiChat.API.RequestModels.Chat;
using ToochiChat.API.Services.Interfaces;
using ToochiChat.Application.Interfaces;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.API.Hubs;

[Authorize]
[SignalRHub]
internal class ChatHub : Hub<IChatClient>
{
    private readonly IConnectionMappingService<string> _usersConnections;
    private readonly IChatService _chatService;
    private readonly IUserService _userService;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(IChatService chatService, IUserService userService, ILogger<ChatHub> logger, 
        IConnectionMappingService<string> usersConnections)
    {
        _chatService = chatService;
        _userService = userService;
        _logger = logger;
        _usersConnections = usersConnections;
    }

    public async Task JoinChat(ChatConnectionModel connection)
    {
        if (_usersConnections.UserExists(connection.UserName))
        {
            _usersConnections.Add(connection.UserName, Context.ConnectionId);
            return;
        }
        
        var chatResult = await _chatService.GetChatById(Guid.Parse(connection.ChatId));
        if (chatResult.IsFailure)
        {
            _logger.LogError(chatResult.Error);
            return;
        }
        
        var addMemberResult = await _chatService.AddChatMember(connection.UserName, connection.ChatId);
        if (addMemberResult.IsFailure)
        {
            _logger.LogError(addMemberResult.Error);
            return;
        }

        _usersConnections.Add(connection.UserName, Context.ConnectionId);

        foreach (var userName in chatResult.Value.Members.Select(m => m.UserName))
        {
            var clients = _usersConnections.GetConnections(userName);
            await Clients
                .Clients(clients)
                .ReceiveNewMember(connection.UserName, DateTime.Now);
        }
    }

    public async Task SendMessage(MessageRequestModel model)
    {
        // TODO: reafctor, add files sending
        
        var membersResult = await _chatService.GetChatMembers(Guid.Parse(model.ChatId));
        if (membersResult.IsFailure) 
        { 
            _logger.LogError(membersResult.Error); 
            return;
        }

        // TODO: replace ALL usernames by userID (GUID)!!!
        var messageResult = Message.CreateNew(Guid.NewGuid(), MessageContent.Create(model.Message.Text).Value,
            model.Message.SentDate);
        if (messageResult.IsFailure)
        {
            _logger.LogError(messageResult.Error);
            return;
        }

        await _chatService.SendMessageFrom(model.UserName, model.ChatId, messageResult.Value);
        
        foreach (var userName in membersResult.Value.Select(m => m.UserName))
        {
            var clients = _usersConnections.GetConnections(userName);
            await Clients
                .Clients(clients)
                .ReceiveMessage(model.UserName, messageResult.Value.Content.Text);
        }
    }

    public async Task DeleteMessage(MessageDeleteRequestModel model)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateMessage(MessageRequestModel model)
    {
        throw new NotImplementedException();
    }
    
    public async Task LeaveChat(ChatConnectionModel connection)
    {
        var chatResult = await _chatService.GetChatById(Guid.Parse(connection.ChatId));
        if (chatResult.IsFailure)
        {
            _logger.LogError(chatResult.Error);
            return;
        }
        
        var removeMemberResult = await _chatService.RemoveChatMember(connection.UserName, connection.ChatId);
        if (removeMemberResult.IsFailure)
        {
            _logger.LogError(removeMemberResult.Error);
            return;
        }
        
        _usersConnections.RemoveUser(connection.UserName);
        
        var otherUsersInChat = 
            chatResult.Value.Members
            .Where(m => m.UserName != connection.UserName)
            .Select(m => m.UserName)
            .ToList();
        
        foreach (var userName in otherUsersInChat)
        {
            var clients = _usersConnections.GetConnections(userName);
            await Clients
                .Clients(clients)
                .ReceiveLeftMember(connection.UserName, DateTime.Now);
        }
    }
}