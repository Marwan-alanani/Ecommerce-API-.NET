namespace Domain.Exceptions;

public sealed class UserNotFoundException(string email)
    : NotFoundException($"No user with email: {email} was found !! ");