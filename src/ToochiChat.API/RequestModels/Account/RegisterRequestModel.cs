using System.ComponentModel.DataAnnotations;

namespace ToochiChat.API.RequestModels.Account;

public sealed record RegisterRequestModel(
    [Required] string Email, 
    [Required] string Password,
    [Required] DateTime BirthDate);