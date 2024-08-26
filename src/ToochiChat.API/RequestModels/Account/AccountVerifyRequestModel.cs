using System.ComponentModel.DataAnnotations;

namespace ToochiChat.API.RequestModels.Account;

public sealed record AccountVerifyRequestModel(
    [Required] string Email, 
    [Required] string ConfirmationCode);