using System.ComponentModel.DataAnnotations;

namespace Shared.Authentication;

public record RegisterRequest(
    string Email,
    string Password,
    string DisplayName,
    string Username,
    string PhoneNumber);