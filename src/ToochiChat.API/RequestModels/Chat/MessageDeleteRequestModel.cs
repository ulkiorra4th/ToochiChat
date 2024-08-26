namespace ToochiChat.API.RequestModels.Chat;

public sealed record MessageDeleteRequestModel(int MessageId, Guid ChatId);