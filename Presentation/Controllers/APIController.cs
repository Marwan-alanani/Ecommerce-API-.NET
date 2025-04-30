global using System.Security.Claims;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class APIController : ControllerBase
{
    protected string GetEmailFromToken() => User.FindFirstValue(claimType: ClaimTypes.Email)!;
}