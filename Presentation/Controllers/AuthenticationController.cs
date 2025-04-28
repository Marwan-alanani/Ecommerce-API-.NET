using Shared.Authentication;

namespace Presentation.Controllers;

public class AuthenticationController(IServiceManager serviceManager)
    : APIController
{
    // [HttpPost]
    // Login (LoginRequest{string email , string password})
    // => User Response {string Token , string Email , string DisplayName}
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
        => Ok(await serviceManager.AuthenticationService.LoginAsync(request));





    // [HttpPost]
    // Register(RegisterRequest {string Email , string username , string password , string DisplayName})
    // => User Response {string Token , string Email , string DisplayName}

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
        => Ok( await serviceManager.AuthenticationService.RegisterAsync(request));

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