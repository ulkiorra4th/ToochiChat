namespace ToochiChat.API.RequestModels;

public sealed class ChatConnectionModel
{
    public required string AccountId { get; set; }
    public required string ChatId { get; set; }
}