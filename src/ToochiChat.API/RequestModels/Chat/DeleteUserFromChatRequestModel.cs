namespace ToochiChat.API.RequestModels.Chat;

public sealed record DeleteUserFromChatRequestModel(Guid DeleterId, Guid UserId);