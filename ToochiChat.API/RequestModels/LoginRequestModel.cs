using System.ComponentModel.DataAnnotations;

namespace ToochiChat.API.RequestModels;

public sealed record LoginRequestModel(
    [Required] string Email, 
    [Required] string Password
    );