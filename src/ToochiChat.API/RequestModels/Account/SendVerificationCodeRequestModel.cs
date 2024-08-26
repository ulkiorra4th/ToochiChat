namespace ToochiChat.API.RequestModels.Account;

public sealed record SendVerificationCodeRequestModel(Guid AccountId, string Email);