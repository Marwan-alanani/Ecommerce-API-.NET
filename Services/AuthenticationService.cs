using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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
        var isValid = await userManager.CheckPasswordAsync(user, request.Password);

        // return a response with a token
        if (isValid) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));

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
        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));
        var errors = result.Errors.Select(e => e.Description).ToList();
        throw new BadRequestException(errors);
    }

    private async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName!),
        };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        string secretKey = "jYBGV2wepcUHAilIRhut7AyLOicr3AZisv9mDCg6Jms=";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); // for encryption
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Myissuer",
            audience: "MyAudience",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}