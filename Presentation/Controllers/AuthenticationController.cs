namespace Presentation.Controllers;

public class AuthenticationController : ControllerBase
{
    // [HttpPost]
    // Login (LoginRequest{string email , string password})
    // => User Response {string Token , string Email , string DisplayName}

    // [HttpPost]
    // Register(RegisterRequest {string Email , string username , string password , string DisplayName})
    // => User Response {string Token , string Email , string DisplayName}

    // [HttpGet]
    // CheckEmail(string email) => bool (registered email address or not)

    // TODO

    // [HttpGet]
    // [Authorize]
    // GetCurrentUserAddress() => AddressDTO

    // [HttpPut]
    // [Authorize]
    // UpdateCurrentUserAddress(AddressDTO) => AddressDTO

    // [HttpGet]
    // [Authorize]
    // GetCurrentUser()
    // => User Response {string Token , string Email , string DisplayName}

}