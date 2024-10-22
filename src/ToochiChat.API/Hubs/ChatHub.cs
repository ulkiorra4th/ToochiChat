using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using ToochiChat.API.Hubs.ClientHubInterfaces;
using ToochiChat.API.RequestModels.Chat;
using ToochiChat.API.Services.Interfaces;
using ToochiChat.Application.Interfaces;
using ToochiChat.Domain.Models.Chatting;
using FileInfo = ToochiChat.Domain.Models.FileInfo;

namespace ToochiChat.API.Hubs;

/// <summary>
/// Hub that handles the chatting
/// </summary>
[Authorize]
[SignalRHub]
internal class ChatHub : Hub<IChatClient>
{
    private readonly IConnectionMappingService<string> _usersConnections;
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(IChatService chatService, ILogger<ChatHub> logger, IConnectionMappingService<string> usersConnections, 
        IMessageService messageService)
    {
        _chatService = chatService;
        _logger = logger;
        _usersConnections = usersConnections;
        _messageService = messageService;
    }

    /// <summary>
    /// Method that executing when user joins to the chat
    /// </summary>
    /// <param name="connection">connection info</param>
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

    /// <summary>
    /// Method that executing when user sends a message
    /// </summary>
    /// <param name="model">all sent message data</param>
    public async Task SendMessage(MessageRequestModel model)
    {
        var membersResult = await _chatService.GetChatMembers(Guid.Parse(model.ChatId));
        if (membersResult.IsFailure) 
        { 
            _logger.LogError(membersResult.Error); 
            return;
        }

        var attachedFiles = model.Message.Media
            .Select(m => FileInfo.Create(String.Empty, m.Type, m.Data).Value)
            .ToList();

        // TODO: replace ALL usernames by userID (GUID)!!!
        var messageResult = Message.CreateNew(Guid.NewGuid(), model.Message.ChatId,
            MessageContent.Create(model.Message.Text, attachedFiles).Value, model.Message.SentDate);

        if (messageResult.IsFailure)
        {
            _logger.LogError(messageResult.Error);
            return;
        }

        await _messageService.Create(messageResult.Value);

        foreach (var userName in membersResult.Value.Select(m => m.UserName))
        {
            var clients = _usersConnections.GetConnections(userName);
            await Clients
                .Clients(clients)
                .ReceiveMessage(model.UserName, messageResult.Value);
        }
    }

    /// <summary>
    /// Method that executing when user deletes a message
    /// </summary>
    /// <param name="model"></param>
    /// <exception cref="NotImplementedException">delete request</exception>
    public async Task DeleteMessage(MessageDeleteRequestModel model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Method that executing when user updates a message
    /// </summary>
    /// <param name="model"></param>
    /// <exception cref="NotImplementedException">all updated message data</exception>
    public async Task UpdateMessage(MessageRequestModel model)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Method that executing when user leaves the chat
    /// </summary>
    /// <param name="connection">connection info</param>
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