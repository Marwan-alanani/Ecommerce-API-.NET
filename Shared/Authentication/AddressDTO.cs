namespace Shared.Authentication;

public class AddressDTO()
{
    public int Id { get; set; } // PK
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}