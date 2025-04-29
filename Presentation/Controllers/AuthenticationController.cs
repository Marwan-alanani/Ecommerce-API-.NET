using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        => Ok(await serviceManager.AuthenticationService.RegisterAsync(request));

    // [HttpGet]
    // CheckEmail(string email) => bool (registered email address or not)
    [HttpGet("CheckEmail")]
    public async Task<ActionResult<bool>> CheckEmail(string email)
        => Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));

    // TODO

    // [HttpGet]
    // [Authorize]
    // GetCurrentUserAddress() => AddressDTO
    [Authorize]
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDTO>> GetAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return await serviceManager.AuthenticationService.GetUserAddressAsync(email!);
    }


    // [HttpPut]
    // [Authorize]
    // UpdateCurrentUserAddress(AddressDTO) => AddressDTO
    [Authorize]
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return await serviceManager.AuthenticationService.UpdateUserAddressAsync(addressDto, email!);
    }

    // [HttpGet]
    // [Authorize]
    // GetCurrentUser()
    // => User Response {string Token , string Email , string DisplayName}


    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await serviceManager.AuthenticationService.GetUserByEmail(email!));
    }
}