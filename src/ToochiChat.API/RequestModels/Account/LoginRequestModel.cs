using System.ComponentModel.DataAnnotations;

namespace ToochiChat.API.RequestModels.Account;

public sealed record LoginRequestModel(
    [Required] string Email, 
    [Required] string Password
    );