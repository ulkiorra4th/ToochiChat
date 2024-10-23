namespace ToochiChat.API.RequestModels.Chat;

public sealed record CreateChatRequestModel(string Title, DateTime CreationDate, ulong OwnerId);