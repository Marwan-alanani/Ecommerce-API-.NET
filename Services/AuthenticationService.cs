namespace Services;

internal class AuthenticationService(UserManager<ApplicationUser> userManager)
    : IAuthenticationService
{
    public async Task<UserResponse> LoginAsync(LoginRequest request)
    {
        // check if there is a user with the email address
        var user = await userManager.FindByEmailAsync(request.Email)
                   ?? throw new UserNotFoundException(request.Email);

        // check the password
        var isValid  =  await userManager.CheckPasswordAsync(user, request.Password);

        // return a response with a token
        if (isValid) return new(request.Email, user.DisplayName , await CreateTokenAsync(user));

        // invalid
        throw new UnauthorizedException();
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            DisplayName = request.DisplayName,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Username
        };
        var result  = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));
        var errors = result.Errors.Select(e => e.Description).ToList();
        throw new BadRequestException(errors);
    }

    private static async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        Task.Delay(TimeSpan.FromSeconds(5)); // just wait 5 seconds
        return "JWTToken";
    }
}