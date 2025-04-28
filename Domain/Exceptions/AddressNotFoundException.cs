namespace Domain.Exceptions;

public  sealed class AddressNotFoundException(string UserName)
    : NotFoundException($"User {UserName} has no Address")
{

}