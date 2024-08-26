namespace ToochiChat.API.RequestModels.Chat;

public sealed record EditChatTitleRequestModel(Guid UserId, string NewTitle);